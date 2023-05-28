namespace Web.Tests.Handlers
{
    using System.Threading;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Web.Commands;
    using Web.Handlers;
    using Web.Infrastructure;
    using Web.Models;
    using Xunit;

    public class UpdateTodoCommandHandlerTests
    {
        private readonly Mock<ILogger<UpdateTodoCommandHandler>> _loggerMock;
        private readonly Mock<ITodoRepository> _todoRepositoryMock;

        public UpdateTodoCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<UpdateTodoCommandHandler>>();
            _todoRepositoryMock = new Mock<ITodoRepository>();
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsValidResponse()
        {
            // Arrange
            var command = new UpdateTodoCommand
            {
                Id = 1,
                Title = "Updated Title",
                IsCompleted = true
            };

            var todoItem = new TodoItem
            {
                Id = command.Id,
                Title = command.Title,
                IsCompleted = command.IsCompleted
            };

            _todoRepositoryMock.Setup(repo => repo.Update(It.IsAny<TodoItem>())).ReturnsAsync(todoItem);
            var handler = new UpdateTodoCommandHandler(_loggerMock.Object, _todoRepositoryMock.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(response.IsValid);
            Assert.Equal(todoItem, response.Result);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsErrorResponse()
        {
            // Arrange
            var command = new UpdateTodoCommand
            {
                Id = 1,
                Title = "Updated Title",
                IsCompleted = true
            };

            var handler = new UpdateTodoCommandHandler(_loggerMock.Object, _todoRepositoryMock.Object);
            _todoRepositoryMock.Setup(repo => repo.Update(It.IsAny<TodoItem>())).ReturnsAsync((TodoItem)null);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(response.IsValid);
            Assert.Null(response.Result);
            Assert.Equal($"No item found with id: {command.Id}", response.Message);
        }
    }
}
