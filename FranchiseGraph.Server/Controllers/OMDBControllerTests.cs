using FranchiseGraph.Server.Model;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using Xunit;

namespace FranchiseGraph.Server.Controllers
{
    public class OMDBControllerTests
    {
        private MockRepository mockRepository;

        private Mock<ILogger<OMDBController>> mockLogger;
        private Mock<IConfiguration> mockConfiguration;
        private Mock<HttpMessageHandler> mockHttpMessageHandler;

        public OMDBControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Loose);

            this.mockLogger = this.mockRepository.Create<ILogger<OMDBController>>();
            this.mockConfiguration = this.mockRepository.Create<IConfiguration>();
            this.mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            this.mockConfiguration.Setup(c => c["TheMovieDB:APIToken"]).Returns("some_valid_api_key");
        }

        private OMDBController CreateOMDBController()
        {
            var httpClient = new HttpClient(this.mockHttpMessageHandler.Object);

            return new OMDBController(
                this.mockLogger.Object,
                this.mockConfiguration.Object,
                httpClient);
        }

        [Fact]
        public async Task GetTMDBCollectionHead_Returns_CollectionResults()
        {
            // Arrange
            var oMDBController = this.CreateOMDBController();

            var collectionResponse = new CollectionResponse
            {
                Results = new List<CollectionResult>
                {
                    new CollectionResult(null, 1241, "Harry Potter Collection", null,
                        "The Harry Potter films are a fantasy series based on the series of seven Harry Potter novels by British writer J. K. Rowling.", null)
                }
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(collectionResponse));
            jsonContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = jsonContent
                });

            // Act
            var result = await oMDBController.GetTMDBCollectionHead("Harry Potter");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CollectionResult>>(result);
            Assert.Single(result);
            Assert.Equal("Harry Potter Collection", result.First().Name);
        }
    }
}