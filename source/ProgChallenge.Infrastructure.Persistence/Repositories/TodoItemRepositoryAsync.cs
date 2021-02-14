using ProgChallenge.Domain.Entities;
using ProgChallenge.Infrastructure.Persistence.Contexts;
using ProgChallenge.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using ProgChallenge.Application.Interfaces.Repositories;

namespace ProgChallenge.Infrastructure.Persistence.Repositories
{
    public class TodoItemRepositoryAsync : GenericRepositoryAsync<TodoItem>, ITodoItemRepositoryAsync
    {
        public TodoItemRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
