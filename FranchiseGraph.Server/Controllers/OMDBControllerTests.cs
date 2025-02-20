using FranchiseGraph.Server.Controllers;
using FranchiseGraph.Server.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
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

            this.mockConfiguration.Setup(c => c["OMDB:ApiKey"]).Returns("some_valid_api_key");
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
        public async Task GetAsync_Get_TMDB_Collection_Head()
        {
            // Arrange
            var oMDBController = this.CreateOMDBController();

            var collectionResult = new CollectionResult("bgp",
               1,
                "jim",
               "el jim",
               "overview",
                "http"
            );

            var collectionResponse = new List<CollectionResponse>
                    {
                        new CollectionResponse(1, new List<CollectionResult> { collectionResult }, 1, 1)
                    };

            var jsonContent = new StringContent(JsonSerializer.Serialize(collectionResponse));

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
            var result = await oMDBController.GetTMDBCollectionHead("spiderman");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CollectionResponse>>(result);
        }
    }
}
