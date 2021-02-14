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
        public TodoListRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
