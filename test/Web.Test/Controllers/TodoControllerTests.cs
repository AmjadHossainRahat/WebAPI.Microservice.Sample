namespace Web.Test.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Web.Commands;
    using Web.Controllers;
    using Web.DTOs;
    using Web.Queries;

    public class TodoControllerTests
    {
        private readonly TodoController _todoController;
        private readonly Mock<ILogger<TodoController>> _loggerMock;
        private readonly Mock<IMediator> _mediatorMock;

        public TodoControllerTests()
        {
            _loggerMock = new Mock<ILogger<TodoController>>();
            _mediatorMock = new Mock<IMediator>();

            _todoController = new TodoController(_loggerMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task Create_ValidCommand_ReturnsOkResult()
        {
            // Arrange
            var command = new CreateTodoCommand();

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateTodoCommand>(), default))
                .ReturnsAsync(new Response { IsValid = true });

            // Act
            var result = await _todoController.Create(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<Response>(okResult.Value);
        }

        [Fact]
        public async Task Create_InvalidCommand_ReturnsBadRequestResult()
        {
            // Arrange
            var command = new CreateTodoCommand();

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateTodoCommand>(), default))
                .ReturnsAsync(new Response { IsValid = false });

            // Act
            var result = await _todoController.Create(command);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsAssignableFrom<Response>(badRequestResult.Value);
        }

        [Fact]
        public async Task Update_ValidIdAndCommand_ReturnsOkResult()
        {
            // Arrange
            var id = 1;
            var command = new UpdateTodoCommand { Id = id };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateTodoCommand>(), default))
                .ReturnsAsync(new Response { IsValid = true });

            // Act
            var result = await _todoController.Update(id, command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<Response>(okResult.Value);
        }

        [Fact]
        public async Task Update_InvalidIdAndCommand_ReturnsBadRequestResult()
        {
            // Arrange
            var id = 1;
            var command = new UpdateTodoCommand { Id = id };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateTodoCommand>(), default))
                .ReturnsAsync(new Response { IsValid = false });

            // Act
            var result = await _todoController.Update(id, command);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsAssignableFrom<Response>(badRequestResult.Value);
        }

        [Fact]
        public async Task Delete_ValidId_ReturnsOkResult()
        {
            // Arrange
            var id = 1;

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteTodoCommand>(), default))
                .ReturnsAsync(new Response { IsValid = true });

            // Act
            var result = await _todoController.Delete(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<Response>(okResult.Value);
        }

        [Fact]
        public async Task Delete_InvalidId_ReturnsBadRequestResult()
        {
            // Arrange
            var id = 1;

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteTodoCommand>(), default))
                .ReturnsAsync(new Response { IsValid = false });

            // Act
            var result = await _todoController.Delete(id);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsAssignableFrom<Response>(badRequestResult.Value);
        }

        [Fact]
        public async Task GetById_ValidId_ReturnsOkResult()
        {
            // Arrange
            var id = 1;

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetTodoQuery>(), default))
                .ReturnsAsync(new Response { IsValid = true });

            // Act
            var result = await _todoController.GetById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<Response>(okResult.Value);
        }

        [Fact]
        public async Task GetById_InvalidId_ReturnsBadRequestResult()
        {
            // Arrange
            var id = 1;

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetTodoQuery>(), default))
                .ReturnsAsync(new Response { IsValid = false });

            // Act
            var result = await _todoController.GetById(id);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsAssignableFrom<Response>(badRequestResult.Value);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult()
        {
            // Arrange

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllTodosQuery>(), default))
                .ReturnsAsync(new Response { IsValid = true });

            // Act
            var result = await _todoController.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<Response>(okResult.Value);
        }

        [Fact]
        public async Task GetAll_Invalid_ReturnsBadRequestResult()
        {
            // Arrange

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllTodosQuery>(), default))
                .ReturnsAsync(new Response { IsValid = false });

            // Act
            var result = await _todoController.GetAll();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsAssignableFrom<Response>(badRequestResult.Value);
        }
    }
}
