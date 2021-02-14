using ProgChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgChallenge.Application.Interfaces.Repositories
{
    public interface ITodoListRepositoryAsync : IGenericRepositoryAsync<TodoList>
    {
        Task<IReadOnlyList<TodoItem>> GetAllTodoItemsAsync(int id, int pageNumber, int pageSize);
        Task<bool> ExistListItemAsync(int todoListId, int todoItemId);
        Task<bool> AddTodoItemAsync(int todoListId, int todoItemId);
        Task<bool> RemoveTodoItemAsync(int todoListId, int todoItemId);
    }
}
