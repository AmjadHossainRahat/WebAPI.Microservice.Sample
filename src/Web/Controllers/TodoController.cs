namespace Web.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Web.Commands;
    using Web.DTOs;
    using Web.Queries;

    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly IMediator _mediator;

        public TodoController(ILogger<TodoController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost(Name = "Create")]
        public async Task<IActionResult> Create(CreateTodoCommand command)
        {
            _logger.Log(LogLevel.Information,"TodoController->Create START");
            Response response = await _mediator.Send(command);
            if (!response.IsValid)
            {
                _logger.Log(LogLevel.Information, "TodoController->Create ENDED");
                return BadRequest(response);
            }

            _logger.Log(LogLevel.Information, "TodoController->Create DONE");
            return Ok(response);
        }

        [HttpPut("{id}", Name = "Update")]
        public async Task<IActionResult> Update(int id, UpdateTodoCommand command)
        {
            _logger.Log(LogLevel.Information, "TodoController->Update START");
            command.Id = id;

            Response response = await _mediator.Send(command);
            if (!response.IsValid)
            {
                _logger.Log(LogLevel.Information, "TodoController->Update ENDED");
                return BadRequest(response);
            }

            _logger.Log(LogLevel.Information, "TodoController->Update DONE");
            return Ok(response);
        }

        [HttpDelete("{id}", Name = "Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.Log(LogLevel.Information, "TodoController->Delete START");
            Response response = await _mediator.Send(new DeleteTodoCommand { Id = id });

            if (!response.IsValid)
            {
                _logger.Log(LogLevel.Information, "TodoController->Delete ENDED");
                return BadRequest(response);
            }

            _logger.Log(LogLevel.Information, "TodoController->Delete DONE");
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetById")]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.Log(LogLevel.Information, "TodoController->GetById START");
            GetTodoQuery query = new GetTodoQuery { Id = id };

            Response response = await _mediator.Send(query);
            if (!response.IsValid)
            {
                _logger.Log(LogLevel.Information, "TodoController->GetById ENDED");
                return BadRequest(response);
            }

            _logger.Log(LogLevel.Information, "TodoController->GetById DONE");
            return Ok(response);
        }

        [HttpGet(Name = "GetAll")]
        [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> GetAll()
        {
            _logger.Log(LogLevel.Information, "TodoController->GetAll START");
            GetAllTodosQuery query = new GetAllTodosQuery();

            Response response = await _mediator.Send(query);
            if (!response.IsValid)
            {
                _logger.Log(LogLevel.Information, "TodoController->GetAll ENDED");
                return BadRequest(response);
            }

            _logger.Log(LogLevel.Information, "TodoController->GetAll DONE");
            return Ok(response);
        }
    }
}
