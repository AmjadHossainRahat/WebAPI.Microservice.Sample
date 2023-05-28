namespace Web.Test.Handlers
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using System.Threading;
    using System.Threading.Tasks;
    using Web.Handlers;
    using Web.Infrastructure;
    using Web.Models;
    using Web.Queries;
    using Xunit;

    public class GetAllTodosQueryHandlerTests
    {
        private readonly Mock<ILogger<GetAllTodosQueryHandler>> _loggerMock;
        private readonly Mock<ITodoRepository> _todoRepositoryMock;
        private GetAllTodosQueryHandler _handler;

        public GetAllTodosQueryHandlerTests()
        {
            _loggerMock = new Mock<ILogger<GetAllTodosQueryHandler>>();
            _todoRepositoryMock = new Mock<ITodoRepository>();
            _handler = new GetAllTodosQueryHandler(_loggerMock.Object, _todoRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsValidResponse()
        {
            // Arrange
            var query = new GetAllTodosQuery { PageNumber = 1, PageSize = 10 };
            var cancellationToken = CancellationToken.None;

            List<TodoItem> items = new List<TodoItem>()
            {
                new TodoItem { Id = 1,},
                new TodoItem { Id = 2,},
                new TodoItem { Id = 3,},
            };

            _todoRepositoryMock
                .Setup(repo => repo.GetAll(query.PageNumber, query.PageSize))
                .ReturnsAsync(((uint)3, items));
            _handler = new GetAllTodosQueryHandler(_loggerMock.Object, _todoRepositoryMock.Object);

            // Act
            var response = await _handler.Handle(query, cancellationToken);

            // Assert
            Assert.True(response.IsValid);
            Assert.NotNull(response.Result);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsInvalidResponse()
        {
            // Arrange
            var query = new GetAllTodosQuery { PageNumber = 0, PageSize = 50 };
            var cancellationToken = CancellationToken.None;

            // Act
            var response = await _handler.Handle(query, cancellationToken);

            // Assert
            Assert.False(response.IsValid);
            Assert.Null(response.Result);
            Assert.Equal("PageNumber must be greater than 0", response.Message);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsInvalidResponse_PageSizeOutOfRange()
        {
            // Arrange
            var query = new GetAllTodosQuery { PageNumber = 1, PageSize = 150 };
            var cancellationToken = CancellationToken.None;

            // Act
            var response = await _handler.Handle(query, cancellationToken);

            // Assert
            Assert.False(response.IsValid);
            Assert.Null(response.Result);
            Assert.Equal("PageSize must be in range 1-100, inclusive", response.Message);
        }

    }

}
