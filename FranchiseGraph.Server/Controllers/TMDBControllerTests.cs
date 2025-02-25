using FranchiseGraph.Server.Model;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using Xunit;

namespace FranchiseGraph.Server.Controllers
{
    public class TMDBControllerTests
    {
        private MockRepository mockRepository;

        private Mock<ILogger<TMDBRequestController>> mockLogger;
        private Mock<IConfiguration> mockConfiguration;
        private Mock<HttpMessageHandler> mockHttpMessageHandler;

        public TMDBControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Loose);

            this.mockLogger = this.mockRepository.Create<ILogger<TMDBRequestController>>();
            this.mockConfiguration = this.mockRepository.Create<IConfiguration>();
            this.mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            this.mockConfiguration.Setup(c => c["TheMovieDB:APIToken"]).Returns("some_valid_api_key");
        }

        private TMDBRequestController CreateOMDBController()
        {
            var httpClient = new HttpClient(this.mockHttpMessageHandler.Object);

            return new TMDBRequestController(
                this.mockLogger.Object,
                this.mockConfiguration.Object,
                httpClient);
        }

        [Fact]
        public async Task getTMDBCollectionHead_Returns_CollectionResults()
        {
            // Arrange
            var oMDBController = this.CreateOMDBController();

            var collectionResponse = new CollectionResponse
            (
               1,
                new List<CollectionResult>
                {
                    new CollectionResult(null, 1241, "Harry Potter Collection", null,
                        "The Harry Potter films are a fantasy series based on the series of seven Harry Potter novels by British writer J. K. Rowling.", null)
                },
                 1,
                 1
            );

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
            var result = await oMDBController.retrieveCollection("Harry Potter");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CollectionResult>>(result);
            Assert.Single(result);
            Assert.Equal("Harry Potter Collection", result.First().Name);
        }

        [Fact]
        public async Task retrieveMoviesFromCollection_Returns_MovieResults()
        {
            // Arrange
            var oMDBController = this.CreateOMDBController();

            var movieResponse = new List<Part>
                {
                    new Part("backdropPath1", 1, "Movie 1", "Movie 1 Original", "Overview 1", "posterPath1", "mediaType1", false, "en", new List<int>{1, 2}, 1.1, "2023-01-01", false, 7.5, 100),
                    new Part("backdropPath2", 2, "Movie 2", "Movie 2 Original", "Overview 2", "posterPath2", "mediaType2", false, "en", new List<int>{3, 4}, 2.2, "2023-02-01", false, 8.0, 200)
                };

            var collResponse = new Collection(123, "Collection 1", "posterPath", "backdropPath", "parts", movieResponse, 5.0);

            var jsonContent = new StringContent(JsonConvert.SerializeObject(movieResponse));

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
            var result = await oMDBController.retrieveMoviesFromCollection(123);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Part>>(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Movie 1", result.First().Title);
            Assert.Equal("Movie 2", result.Last().Title);
        }
    }
}