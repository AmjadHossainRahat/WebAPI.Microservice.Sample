namespace Web.Commands
{
    using MediatR;
    using Web.DTOs;
    using Web.Models;

    public class UpdateTodoCommand : IRequest<Response>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }
    }

}
