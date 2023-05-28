namespace Web.Tests.Handlers
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using System.Threading;
    using System.Threading.Tasks;
    using Web.Commands;
    using Web.DTOs;
    using Web.Handlers;
    using Web.Infrastructure;
    using Web.Models;
    using Xunit;

    public class CreateTodoCommandHandlerTests
    {
        private readonly Mock<ILogger<CreateTodoCommandHandler>> _loggerMock;
        private readonly Mock<ITodoRepository> _todoRepositoryMock;
        private CreateTodoCommandHandler _handler;

        public CreateTodoCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<CreateTodoCommandHandler>>();
            _todoRepositoryMock = new Mock<ITodoRepository>();
            _handler = new CreateTodoCommandHandler(_loggerMock.Object, _todoRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsValidResponse()
        {
            // Arrange
            var command = new CreateTodoCommand { Title = "Test Title", IsCompleted = false };
            var todoItem = new TodoItem { Id = 1, Title = "Test Title", IsCompleted = false };
            var response = new Response { IsValid = true };

            _todoRepositoryMock.Setup(repo => repo.Create(todoItem)).ReturnsAsync(todoItem);
            _handler = new CreateTodoCommandHandler(_loggerMock.Object, _todoRepositoryMock.Object);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}
