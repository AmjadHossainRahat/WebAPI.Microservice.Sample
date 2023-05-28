namespace Web.Handlers
{
    using MediatR;
    using Web.Commands;
    using Web.DTOs;
    using Web.Infrastructure;
    using Web.Models;

    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Response>
    {
        private readonly ILogger<CreateTodoCommandHandler> _logger;
        private readonly ITodoRepository _todoRepository;

        public CreateTodoCommandHandler(
            ILogger<CreateTodoCommandHandler> logger,
            ITodoRepository todoRepository)
        {
            _logger = logger;
            _todoRepository = todoRepository;
        }

        public async Task<Response> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateTodoCommandHandler->Handle START");

            Response response = new Response { IsValid = true };

            TodoItem todoItem = this.Mapper(request);

            response.Result = await _todoRepository.Create(todoItem);

            _logger.LogInformation("CreateTodoCommandHandler->Handle DONE");
            return response;
        }

        private TodoItem Mapper(CreateTodoCommand request)
        {
            _logger.LogInformation("CreateTodoCommandHandler->Mapper called");
            return new TodoItem
            {
                Title = request.Title,
                IsCompleted = request.IsCompleted
            };
        }
    }
}
