using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgChallenge.Application.Interfaces;
using ProgChallenge.Application.Interfaces.Repositories;
using ProgChallenge.Infrastructure.Persistence.Contexts;
using ProgChallenge.Infrastructure.Persistence.Repositories;
using ProgChallenge.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgChallenge.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<ITodoItemRepositoryAsync, TodoItemRepositoryAsync>();
            services.AddTransient<ITodoListRepositoryAsync, TodoListRepositoryAsync>();
            #endregion
        }
    }
}
