using Microsoft.EntityFrameworkCore;
using ProgChallenge.Application.Interfaces.Repositories;
using ProgChallenge.Domain.Entities;
using ProgChallenge.Infrastructure.Persistence.Contexts;
using ProgChallenge.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgChallenge.Infrastructure.Persistence.Repositories
{
    public class TodoListRepositoryAsync : GenericRepositoryAsync<TodoList>, ITodoListRepositoryAsync
    {
        private readonly DbSet<TodoList> _todoList;
        private readonly DbSet<TodoItem> _todoItem;

        public TodoListRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _todoList = dbContext.Set<TodoList>();
            _todoItem = dbContext.Set<TodoItem>();
        }

        public async Task<IReadOnlyList<TodoItem>> GetAllTodoItemsAsync(int id, int pageNumber, int pageSize)
        {
            var todoItem = await GetByIdAsync(id);
            var todoItems = todoItem.Items;

            return todoItems
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public async Task<bool> ExistListItemAsync(int todoListId, int todoItemId)
        {
            var todoList = await _todoList
                 .FindAsync(todoListId);

            var exist = todoList != null &&
                        todoList.Items != null && 
                        todoList.Items.Count(x => x.Id == todoItemId) > 0;

            return exist;
        }

        public async Task<bool> AddTodoItemAsync(int todoListId, int todoItemId)
        {
            var todoItem = await _todoItem
                 .FindAsync(todoItemId);

            var todoList = await _todoList
                .FindAsync(todoListId);

            if (todoItem == default || todoList == default)
                return false;

            todoList.Items.Add(todoItem);
            await UpdateAsync(todoList);
            return true;
        }

        public async Task<bool> RemoveTodoItemAsync(int todoListId, int todoItemId)
        {
            var todoItem = await _todoItem
                 .FindAsync(todoItemId);

            var todoList = await _todoList
                .FindAsync(todoListId);

            todoList.Items.Remove(todoItem);
            await UpdateAsync(todoList);
            return true;
        }
    }
}
