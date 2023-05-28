namespace Web.Commands
{
    using MediatR;
    using Web.DTOs;

    public class DeleteTodoCommand : IRequest<Response>
    {
        public int Id { get; set; }
    }
}
