using ProgChallenge.Application.Interfaces;
using ProgChallenge.Domain.Entities;
using System.Threading.Tasks;

namespace ProgChallenge.Application.Interfaces.Repositories
{
    public interface ITodoItemRepositoryAsync : IGenericRepositoryAsync<TodoItem>
    {
        Task<bool> AddAsync(TodoItem todoItem, int todoListId);
    }
}
