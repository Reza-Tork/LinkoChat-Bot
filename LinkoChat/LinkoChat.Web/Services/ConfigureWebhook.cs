using LinkoChat.Application.Configuration;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;

namespace LinkoChat.Web.Services
{
    public class ConfigureWebhook(
        ILogger<ConfigureWebhook> logger,
        IServiceProvider serviceProvider,
        IConfiguration configuration) : IHostedService
    {
        private readonly ILogger<ConfigureWebhook> _logger = logger;
        private readonly IServiceProvider _services = serviceProvider;
        private readonly BotConfiguration _botConfig = configuration.GetSection("BotConfiguration").Get<BotConfiguration>();

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

            var webhookAddress = @$"{_botConfig.HostAddress}/bot/{_botConfig.MySecretPath}";

            _logger.LogInformation("Setting webhook: {webhookAddress}", webhookAddress);

            await botClient.SetWebhookAsync(
                url: webhookAddress,
                allowedUpdates: Array.Empty<UpdateType>(),
                cancellationToken: cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

            // Remove webhook upon app shutdown
            _logger.LogInformation("Removing webhook");
            await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
        }
    }
}
