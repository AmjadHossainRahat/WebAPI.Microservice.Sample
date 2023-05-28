namespace Web.Infrastructure
{
    using Web.Models;

    public interface ITodoRepository
    {
        Task<(uint, List<TodoItem>)> GetAll(ushort pageNumber, ushort pageSize);
        Task<TodoItem> GetById(int id);
        Task<TodoItem> Create(TodoItem item);
        Task<TodoItem> Update(TodoItem item);
        Task<bool> Delete(int id);
    }
}
