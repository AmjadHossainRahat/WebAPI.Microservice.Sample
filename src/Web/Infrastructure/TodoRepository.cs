namespace Web.Infrastructure
{
    using System.Collections.Generic;
    using Web.Models;

    public class TodoRepository : ITodoRepository
    {
        private readonly List<TodoItem> _todos;
        private int _nextId;

        public TodoRepository()
        {
            _todos = new List<TodoItem>();
            _nextId = 1;
        }

        public async Task<(uint, List<TodoItem>)> GetAll(ushort pageNumber, ushort pageSize)
        {
            int pagingStart = ((pageNumber - 1) * pageSize) + 1;
            int startIndex = pagingStart - 1;
            startIndex = startIndex >= 0 ? startIndex : 0;

            List<TodoItem> paginatedData = _todos.GetRange(startIndex, pageSize);

            return ((uint, List<TodoItem>))await Task.FromResult((_todos.Count, paginatedData));
        }

        public async Task<TodoItem> GetById(int id)
        {
            return await Task.FromResult(_todos?.Find(p => p.Id == id));
        }

        public async Task<TodoItem> Create(TodoItem item)
        {
            item.Id = _nextId++;
            _todos.Add(item);

            return await Task.FromResult(item);
        }

        public async Task<TodoItem> Update(TodoItem item)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == item.Id);

            if (todo != null)
            {
                todo.Title = item.Title;
                todo.IsCompleted = item.IsCompleted;
            }

            return await Task.FromResult(todo);
        }

        public async Task<bool> Delete(int id)
        {
            int indx = _todos.FindIndex(p => p.Id == id);
            if (indx > -1)
            {
                _todos.RemoveAt(indx);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}
