using FranchiseGraph.Server.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FranchiseGraph.Server.Controllers
{
    public class OMDBControllerTests
    {
        private MockRepository mockRepository;

        private Mock<ILogger<OMDBController>> mockLogger;
        private Mock<IConfiguration> mockConfiguration;

        public OMDBControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockLogger = this.mockRepository.Create<ILogger<OMDBController>>();
            this.mockConfiguration = this.mockRepository.Create<IConfiguration>();
        }

        private OMDBController CreateOMDBController()
        {
            return new OMDBController(
                this.mockLogger.Object,
                this.mockConfiguration.Object);
        }

        [Fact]
        public async Task GetAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var oMDBController = this.CreateOMDBController();

            // Act
            var result = await oMDBController.GetAsync();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void GetAsync_gets_results()
        {
            // Arrange
            var oMDBController = this.CreateOMDBController();

            var result = oMDBController.GetAsync();


        }

        [Fact]
        public async Task GetAsync_ReturnsSingleOMDBResponse_WhenSearchQueryIsValid()
        {
            // Arrange
            var oMDBController = this.CreateOMDBController();
            var apiKey = "some_valid_api_key";
            this.mockConfiguration.Setup(c => c["OMDB:ApiKey"]).Returns(apiKey);
            var searchQuery = "?t=spiderman";
            // Act
            var result = await oMDBController.GetAsync();
            // Assert
            Assert.Single(result);
            Assert.Equal(searchQuery, null); //result.First().Title);
        }

    }
}
