using LinkoChat.Application.Interfaces;
using LinkoChat.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddTelegramMiddleware(this IServiceCollection services)
        {
            services.AddScoped<IHandleUpdateService, HandleUpdateService>();

            return services;
        }
    }
}
