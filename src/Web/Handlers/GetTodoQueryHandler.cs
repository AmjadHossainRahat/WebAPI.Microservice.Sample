namespace Web.Handlers
{
    using MediatR;
    using Web.DTOs;
    using Web.Infrastructure;
    using Web.Models;
    using Web.Queries;

    public class GetTodoQueryHandler : IRequestHandler<GetTodoQuery, Response?>
    {
        private readonly ILogger<GetTodoQueryHandler> _logger;
        private readonly ITodoRepository _todoRepository;

        public GetTodoQueryHandler(
            ILogger<GetTodoQueryHandler> logger,
            ITodoRepository todoRepository)
        {
            _logger = logger;
            _todoRepository = todoRepository;
        }

        public async Task<Response?> Handle(GetTodoQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetTodoQueryHandler->Handle START");

            Response response = new Response { IsValid = true };

            TodoItem todoItem = await _todoRepository.GetById(request.Id);
            if (todoItem == null)
            {
                response.IsValid = false;
                response.Message = $"No item found with id: {request.Id}";

                _logger.LogInformation("GetTodoQueryHandler->Handle ENDED");
                return response;
            }

            response.Result = todoItem;

            _logger.LogInformation("GetTodoQueryHandler->Handle DONE");
            return response;
        }
    }
}
