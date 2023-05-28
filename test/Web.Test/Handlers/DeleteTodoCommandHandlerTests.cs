namespace Web.Tests.Handlers
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using System.Threading;
    using System.Threading.Tasks;
    using Web.Commands;
    using Web.Handlers;
    using Web.Infrastructure;
    using Xunit;

    public class DeleteTodoCommandHandlerTests
    {
        private readonly Mock<ILogger<DeleteTodoCommandHandler>> _loggerMock;
        private readonly Mock<ITodoRepository> _todoRepositoryMock;

        private readonly DeleteTodoCommandHandler _handler;

        public DeleteTodoCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<DeleteTodoCommandHandler>>();
            _todoRepositoryMock = new Mock<ITodoRepository>();
            _handler = new DeleteTodoCommandHandler(_loggerMock.Object, _todoRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidId_ReturnsValidResponse()
        {
            // Arrange
            var command = new DeleteTodoCommand { Id = 1 };
            _todoRepositoryMock.Setup(repo => repo.Delete(command.Id)).ReturnsAsync(true);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(response.IsValid);
            Assert.Null(response.Message);
        }

        [Fact]
        public async Task Handle_InvalidId_ReturnsInvalidResponse()
        {
            // Arrange
            var command = new DeleteTodoCommand { Id = 1 };
            _todoRepositoryMock.Setup(repo => repo.Delete(command.Id)).ReturnsAsync(false);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(response.IsValid);
            Assert.Equal($"No item was found with id: {command.Id}", response.Message);
        }
    }
}
