using LinkoChat.Application.Configuration;
using LinkoChat.Application.Interfaces;
using LinkoChat.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace LinkoChat.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var botConfig = hostContext.Configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
                    var connectionString = hostContext.Configuration.GetConnectionString("Default");

                    ConfigureServices(services, botConfig, connectionString);
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .UseConsoleLifetime();

            var host = builder.Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var botConfig = services.GetRequiredService<IOptions<BotConfiguration>>().Value;
                    var handleUpdateService = services.GetRequiredService<IHandleUpdateService>();
                    var botClient = services.GetRequiredService<ITelegramBotClient>();
                    var receiverOptions = new ReceiverOptions
                    {
                        AllowedUpdates = [] // receive all update types
                    };

                    botClient.StartReceiving(
                        updateHandler: async (client, update, token) => await handleUpdateService.HandleUpdate(update),
                        pollingErrorHandler: (client, exception, token) =>
                        {
                            var logger = services.GetRequiredService<ILogger<Program>>();
                            logger.LogError(exception, "An error occurred while receiving updates.");
                            return Task.CompletedTask;
                        },
                        receiverOptions: receiverOptions,
                        cancellationToken: default
                    );

                    Console.WriteLine("Bot is up and running. Press any key to exit.");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred.");
                }
            }
        }

        private static void ConfigureServices(IServiceCollection services, BotConfiguration botConfig, string connectionString)
        {
            var httpClientHandler = new HttpClientHandler
            {
                Proxy = new WebProxy("http://127.0.0.1:2081"),
                UseProxy = true,
            };

            services.AddHttpClient<ITelegramBotClient, TelegramBotClient>((httpClient, sp) =>
                new TelegramBotClient(botConfig.BotToken, httpClient)
            )
            .ConfigurePrimaryHttpMessageHandler(() => httpClientHandler);

            services.AddDependencies(connectionString);

            services.Configure<BotConfiguration>(options =>
            {
                options.BotToken = botConfig.BotToken;
                options.MySecretPath = botConfig.MySecretPath;
            });

            services.AddLogging();
        }
    }
}
