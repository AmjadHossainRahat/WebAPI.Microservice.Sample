namespace Web.Handlers
{
    using MediatR;
    using Web.Commands;
    using Web.DTOs;
    using Web.Infrastructure;

    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, Response>
    {
        private readonly ILogger<DeleteTodoCommandHandler> _logger;
        private readonly ITodoRepository _todoRepository;

        public DeleteTodoCommandHandler(
            ILogger<DeleteTodoCommandHandler> logger,
            ITodoRepository todoRepository)
        {
            _logger = logger;
            _todoRepository = todoRepository;
        }

        public async Task<Response> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UpdateTodoCommandHandler->Handle START");
            Response response = new Response();

            response.IsValid = await _todoRepository.Delete(request.Id);
            if (!response.IsValid)
            {
                response.Message = $"No item was found with id: {request.Id}";
            }

            _logger.LogInformation("UpdateTodoCommandHandler->Handle START");
            return response;
        }
    }

}
