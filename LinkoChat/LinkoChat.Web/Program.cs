using LinkoChat.Application.Configuration;
using LinkoChat.Application.Interfaces;
using LinkoChat.Web.Services;
using LinkoChat.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Text.Json;

namespace LinkoChat.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var botConfig = ConfigureBotSettings(builder.Configuration);
            var connectionString = ConfigureConnectionString(builder.Configuration);

            ConfigureServices(builder.Services, botConfig, connectionString);

            var app = builder.Build();

            InitializeDatabase(app.Services);

            ConfigureMiddleware(app, botConfig);

            app.Run();
        }

        private static BotConfiguration ConfigureBotSettings(IConfiguration configuration)
        {
            var botConfig = configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
            if (botConfig?.BotToken == null)
            {
                throw new ArgumentNullException(nameof(botConfig.BotToken), "Bot token cannot be null");
            }
            return botConfig;
        }

        private static string ConfigureConnectionString(IConfiguration configuration)
        {
            return configuration.GetConnectionString("Default") 
                   ?? throw new ArgumentNullException("ConnectionString", "ConnectionString cannot be null");
        }

        private static void ConfigureServices(IServiceCollection services, BotConfiguration botConfig, string connectionString)
        {
            services.AddHttpClient("TelegramWebhook")
                .AddTypedClient<ITelegramBotClient>(httpClient => new TelegramBotClient(botConfig.BotToken, httpClient));

            services.AddDependencies(connectionString);

            services.Configure<BotConfiguration>(options =>
            {
                options.BotToken = botConfig.BotToken;
                // Configure other BotConfiguration properties if needed
            });

            services.AddHostedService<ConfigureWebhook>();
        }

        private static void InitializeDatabase(IServiceProvider services)
        {
            DatabaseInitializer.EnsureDatabaseCreated(services);
        }

        private static void ConfigureMiddleware(WebApplication app, BotConfiguration botConfig)
        {
            app.UseHttpsRedirection();
            app.UseRouting();

            app.MapPost($"/bot/{botConfig.MySecretPath}", async context =>
            {
                var update = await DeserializeRequestBody<Update>(context.Request);

                if (update?.Message == null)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid update payload");
                    return;
                }

                var botClient = context.RequestServices.GetRequiredService<ITelegramBotClient>();
                var handleUpdateService = context.RequestServices.GetRequiredService<IHandleUpdateService>();

                await handleUpdateService.HandleUpdate(update);
                context.Response.StatusCode = 200;

            }).WithName("TelegramWebhook");
        }

        private static async Task<T?> DeserializeRequestBody<T>(HttpRequest request) where T : class
        {
            try
            {
                using var reader = new StreamReader(request.Body);
                var body = await reader.ReadToEndAsync();

                if (string.IsNullOrWhiteSpace(body))
                {
                    return null;
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var deserializedObject = JsonSerializer.Deserialize<T>(body, options);
                return deserializedObject;
            }
            catch (JsonException)
            {
                return null;
            }
        }

    }
}
