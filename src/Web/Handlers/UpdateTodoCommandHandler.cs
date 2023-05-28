namespace Web.Handlers
{
    using MediatR;
    using Web.Commands;
    using Web.DTOs;
    using Web.Infrastructure;
    using Web.Models;

    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, Response>
    {
        private readonly ILogger<UpdateTodoCommandHandler> _logger;
        private readonly ITodoRepository _todoRepository;

        public UpdateTodoCommandHandler(
            ILogger<UpdateTodoCommandHandler> logger, 
            ITodoRepository todoRepository)
        {
            _logger = logger;
            _todoRepository = todoRepository;
        }

        public async Task<Response> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UpdateTodoCommandHandler->Handle START");
            Response response = new Response { IsValid = true };

            TodoItem todoItem = this.Mapper(request);
            response.Result = await _todoRepository.Update(todoItem);

            if (response.Result == null)
            {
                response.IsValid= false;
                response.Message = $"No item found with id: {request.Id}";
            }

            _logger.LogInformation("UpdateTodoCommandHandler->Handle DONE");
            return response;
        }

        private TodoItem Mapper(UpdateTodoCommand request)
        {
            _logger.LogInformation("UpdateTodoCommandHandler->Mapper called");
            return new TodoItem
            {
                Id = request.Id,
                Title = request.Title,
                IsCompleted = request.IsCompleted
            };
        }
    }
}
