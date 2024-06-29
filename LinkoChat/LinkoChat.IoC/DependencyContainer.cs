using LinkoChat.Application.Interfaces;
using LinkoChat.Application.Services;
using LinkoChat.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace LinkoChat.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddScoped<IHandleUpdateService, HandleUpdateService>();

            return services;
        }
    }
}
