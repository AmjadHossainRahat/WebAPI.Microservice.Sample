namespace Web.Queries
{
    using MediatR;
    using Web.DTOs;
    using Web.Models;

    public class GetTodoQuery : IRequest<Response>
    {
        public int Id { get; set; }
    }
}
