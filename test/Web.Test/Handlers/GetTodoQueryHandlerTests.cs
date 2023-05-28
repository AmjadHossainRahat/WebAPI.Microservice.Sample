using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Web.Handlers;
using Web.Infrastructure;
using Web.Models;
using Web.Queries;
using Xunit;

namespace Web.Tests.Handlers
{
    public class GetTodoQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ValidId_ReturnsValidResponse()
        {
            // Arrange
            int todoId = 1;
            var mockLogger = new Mock<ILogger<GetTodoQueryHandler>>();
            var mockTodoRepository = new Mock<ITodoRepository>();
            var handler = new GetTodoQueryHandler(mockLogger.Object, mockTodoRepository.Object);

            TodoItem todoItem = new TodoItem { Id = todoId, Title = "Sample Todo" };
            mockTodoRepository.Setup(r => r.GetById(todoId)).ReturnsAsync(todoItem);

            var query = new GetTodoQuery { Id = todoId };
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.Equal(todoItem, result.Result);
            Assert.Null(result.Message);
        }

        [Fact]
        public async Task Handle_InvalidId_ReturnsInvalidResponse()
        {
            // Arrange
            int todoId = 1;
            var mockLogger = new Mock<ILogger<GetTodoQueryHandler>>();
            var mockTodoRepository = new Mock<ITodoRepository>();
            var handler = new GetTodoQueryHandler(mockLogger.Object, mockTodoRepository.Object);

            mockTodoRepository.Setup(r => r.GetById(todoId)).ReturnsAsync((TodoItem)null);

            var query = new GetTodoQuery { Id = todoId };
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Null(result.Result);
            Assert.Equal($"No item found with id: {todoId}", result.Message);
        }
    }
}
