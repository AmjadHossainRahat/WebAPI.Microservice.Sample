namespace Web.Tests.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Web.Infrastructure;
    using Web.Models;
    using Xunit;

    public class TodoRepositoryTests
    {
        [Fact]
        public async Task GetAll_ShouldReturnCorrectPaginatedData()
        {
            // Arrange
            var repository = new TodoRepository();
            var todos = new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "Todo 1", IsCompleted = false },
                new TodoItem { Id = 2, Title = "Todo 2", IsCompleted = true },
                new TodoItem { Id = 3, Title = "Todo 3", IsCompleted = false },
                new TodoItem { Id = 4, Title = "Todo 4", IsCompleted = true },
                new TodoItem { Id = 5, Title = "Todo 5", IsCompleted = false },
            };
            repository.SetupTodos(todos);

            ushort pageNumber = 2;
            ushort pageSize = 2;

            // Act
            var result = await repository.GetAll(pageNumber, pageSize);

            // Assert
            Assert.Equal((uint)5, result.Item1); // Total item count
            Assert.Equal(2, result.Item2.Count); // Page size
            Assert.Equal(3, result.Item2[0].Id); // First item on the page
            Assert.Equal(4, result.Item2[1].Id); // Second item on the page
        }

        [Fact]
        public async Task GetById_ExistingId_ShouldReturnMatchingTodo()
        {
            // Arrange
            var repository = new TodoRepository();
            var todos = new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "Todo 1", IsCompleted = false },
                new TodoItem { Id = 2, Title = "Todo 2", IsCompleted = true },
                new TodoItem { Id = 3, Title = "Todo 3", IsCompleted = false },
            };
            repository.SetupTodos(todos);

            int id = 2;

            // Act
            var result = await repository.GetById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetById_NonExistingId_ShouldReturnNull()
        {
            // Arrange
            var repository = new TodoRepository();
            var todos = new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "Todo 1", IsCompleted = false },
                new TodoItem { Id = 2, Title = "Todo 2", IsCompleted = true },
                new TodoItem { Id = 3, Title = "Todo 3", IsCompleted = false },
            };
            repository.SetupTodos(todos);

            int id = 4;

            // Act
            var result = await repository.GetById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Create_ShouldAddNewItemToRepository()
        {
            // Arrange
            var repository = new TodoRepository();
            var item = new TodoItem { Title = "New Todo", IsCompleted = false };

            // Act
            var result = await repository.Create(item);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Single(repository.GetTodos());
            Assert.Equal(item.Title, repository.GetTodos().First().Title);
        }

        [Fact]
        public async Task Update_ExistingTodo_ShouldUpdateTodoProperties()
        {
            // Arrange
            var repository = new TodoRepository();
            var todos = new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "Todo 1", IsCompleted = false },
                new TodoItem { Id = 2, Title = "Todo 2", IsCompleted = true },
                new TodoItem { Id = 3, Title = "Todo 3", IsCompleted = false },
            };
            repository.SetupTodos(todos);

            var updatedItem = new TodoItem { Id = 2, Title = "Updated Todo", IsCompleted = false };

            // Act
            var result = await repository.Update(updatedItem);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedItem.Title, repository.GetTodos().First(t => t.Id == updatedItem.Id).Title);
            Assert.Equal(updatedItem.IsCompleted, repository.GetTodos().First(t => t.Id == updatedItem.Id).IsCompleted);
        }

        [Fact]
        public async Task Update_NonExistingTodo_ShouldReturnNull()
        {
            // Arrange
            var repository = new TodoRepository();
            var todos = new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "Todo 1", IsCompleted = false },
                new TodoItem { Id = 2, Title = "Todo 2", IsCompleted = true },
                new TodoItem { Id = 3, Title = "Todo 3", IsCompleted = false },
            };
            repository.SetupTodos(todos);

            var updatedItem = new TodoItem { Id = 4, Title = "Updated Todo", IsCompleted = false };

            // Act
            var result = await repository.Update(updatedItem);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ExistingTodo_ShouldRemoveTodoFromRepository()
        {
            // Arrange
            var repository = new TodoRepository();
            var todos = new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "Todo 1", IsCompleted = false },
                new TodoItem { Id = 2, Title = "Todo 2", IsCompleted = true },
                new TodoItem { Id = 3, Title = "Todo 3", IsCompleted = false },
            };
            repository.SetupTodos(todos);

            int id = 2;

            // Act
            var result = await repository.Delete(id);

            // Assert
            Assert.True(result);
            Assert.Equal(2, repository.GetTodos().Count);
            Assert.Null(repository.GetTodos().FirstOrDefault(t => t.Id == id));
        }

        [Fact]
        public async Task Delete_NonExistingTodo_ShouldReturnFalse()
        {
            // Arrange
            var repository = new TodoRepository();
            var todos = new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "Todo 1", IsCompleted = false },
                new TodoItem { Id = 2, Title = "Todo 2", IsCompleted = true },
                new TodoItem { Id = 3, Title = "Todo 3", IsCompleted = false },
            };
            repository.SetupTodos(todos);

            int id = 4;

            // Act
            var result = await repository.Delete(id);

            // Assert
            Assert.False(result);
            Assert.Equal(3, repository.GetTodos().Count);
        }
    }

    // Extension method to set up todos in the repository for testing
    public static class TodoRepositoryExtensions
    {
        public static void SetupTodos(this TodoRepository repository, List<TodoItem> todos)
        {
            repository.GetType()
                      .GetField("_todos", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                      .SetValue(repository, todos);

            repository.GetType()
                      .GetField("_nextId", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                      .SetValue(repository, todos.Max(t => t.Id) + 1);
        }

        public static List<TodoItem> GetTodos(this TodoRepository repository)
        {
            return repository.GetType()
                             .GetField("_todos", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                             .GetValue(repository) as List<TodoItem>;
        }
    }
}
