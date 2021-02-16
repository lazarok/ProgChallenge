using ProgChallenge.Domain.Entities;
using ProgChallenge.Infrastructure.Persistence.Contexts;
using ProgChallenge.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using ProgChallenge.Application.Interfaces.Repositories;
using System.Threading.Tasks;

namespace ProgChallenge.Infrastructure.Persistence.Repositories
{
    public class TodoItemRepositoryAsync : GenericRepositoryAsync<TodoItem>, ITodoItemRepositoryAsync
    {

        private readonly DbSet<TodoList> _todoList;
        private readonly DbSet<TodoItem> _todoItem;

        private readonly ITodoListRepositoryAsync _todoListRepository;

        public TodoItemRepositoryAsync(ApplicationDbContext dbContext, ITodoListRepositoryAsync todoListRepository) : base(dbContext)
        {
            _todoList = dbContext.Set<TodoList>();
            _todoItem = dbContext.Set<TodoItem>();
            _todoListRepository = todoListRepository;
        }

        public async Task<bool> AddAsync(TodoItem todoItem, int todoListId)
        {
            var newtodoItem = await AddAsync(todoItem);
            return await _todoListRepository.AddTodoItemAsync(todoListId, newtodoItem.Id);
        }
    }
}
