using MediatR;
using Web.DTOs;
using Web.Models;

namespace Web.Queries
{
    public class GetAllTodosQuery : IRequest<Response>
    {
        public ushort PageNumber { get; set; } = 0;
        public ushort PageSize { get; set; } = 10;
    }
}
