namespace Web.Handlers
{
    using MediatR;
    using Web.DTOs;
    using Web.Infrastructure;
    using Web.Models;
    using Web.Queries;

    public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, Response>
    {
        private readonly ILogger<GetAllTodosQueryHandler> _logger;
        private readonly ITodoRepository _todoRepository;

        public GetAllTodosQueryHandler(
            ILogger<GetAllTodosQueryHandler> logger,
            ITodoRepository todoRepository)
        {
            _logger = logger;
            _todoRepository = todoRepository;
        }

        public async Task<Response> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllTodosQueryHandler->Handle START");
            Response response = this.ValidateRequest(request);
            if (!response.IsValid)
            {
                _logger.LogInformation("GetAllTodosQueryHandler->Handle ENDED");
                return response;
            }

            (uint totalCount, List<TodoItem> data) = await _todoRepository.GetAll(request.PageNumber, request.PageSize);

            response.IsValid = true;
            response.Result = new { totalCount = totalCount, data = data };

            _logger.LogInformation("GetAllTodosQueryHandler->Handle DONE");
            return response;
        }

        private Response ValidateRequest(GetAllTodosQuery request)
        {
            _logger.LogInformation("GetAllTodosQueryHandler->ValidateRequest START");
            Response response = new Response { IsValid = false };
            if (request.PageNumber < 1)
            {
                response.Message = "PageNumber must be greater than 0";
            }
            else if (request.PageSize < 1 || request.PageSize > 100)
            {
                response.Message = "PageSize must be in range 1-100, inclusive";
            }
            else
            {
                response.IsValid = true;
            }

            _logger.LogInformation($"GetAllTodosQueryHandler->ValidateRequest DONE", response);
            return response;
        }
    }
}
