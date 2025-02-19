using FranchiseGraph.Server.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System;
using System.Net;
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
        public async Task GetAsync_gets_results_of_right_type()
        {
            // Arrange
            var oMDBController = this.CreateOMDBController();
            var expectedUrl = "https://www.omdbapi.com/?apikey=some_valid_api_key&s=spiderman";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{ \"Title\":\"Spiderman\",\"Year\":\"2002\"}") // Return a valid JSON response
                });
            // Act
            var result = await oMDBController.GetAsync("spiderman");

            //Assert.IsType<List<OMDBResponse>>(result);

            Assert.NotNull(result);
            Assert.IsType<List<OMDBResponse>>(result);
        }
    }
}
