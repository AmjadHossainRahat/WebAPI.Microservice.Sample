namespace Web.Test.Controllers
{
    using cqrs.mediator.repository.pattern.Controllers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;

    public class HealthControllerTests
    {
        private readonly Mock<ILogger<HealthController>> _logger;
        private readonly HealthController _healthController;

        public HealthControllerTests()
        {
            _logger = new Mock<ILogger<HealthController>>();
            _healthController = new HealthController(_logger.Object);
        }

        [Fact]
        public async Task Create_ValidCommand_ReturnsOkWithHealthy()
        {
            // Arrange
            string expectedResponse = "healthy";

            // Act
            var result = await _healthController.Get() as string;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse, result);
        }
    }
}
