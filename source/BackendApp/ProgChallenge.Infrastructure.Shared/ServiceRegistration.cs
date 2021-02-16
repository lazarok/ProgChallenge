using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgChallenge.Application.Interfaces;
using ProgChallenge.Infrastructure.Shared.Services;

namespace ProgChallenge.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
        }
    }
}
