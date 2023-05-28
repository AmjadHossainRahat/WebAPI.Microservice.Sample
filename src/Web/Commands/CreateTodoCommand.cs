namespace Web.Commands
{
    using MediatR;
    using Web.DTOs;

    public class CreateTodoCommand : IRequest<Response>
    {
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
