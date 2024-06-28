using LinkoChat.Application.Configuration;
using LinkoChat.Application.Interfaces;
using LinkoChat.Application.Services;
using LinkoChat.Web.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace LinkoChat.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var botConfig = builder.Configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
            var botToken = botConfig?.BotToken ?? string.Empty;

            builder.Services.AddAuthorization();

            builder.Services.AddHttpClient("TelegramWebhook")
                .AddTypedClient<ITelegramBotClient>(httpClient => new TelegramBotClient(botToken, httpClient));

            builder.Services.AddHostedService<ConfigureWebhook>();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapPost($"/bot/{botConfig?.MySecretPath}", async (
    ITelegramBotClient botClient,
    HttpRequest request,
    IHandleUpdateService handleUpdateService,
    Update update) =>
            {
                if (update.Message == null)
                {
                    throw new ArgumentException(nameof(update.Message));
                }

                await handleUpdateService.HandleUpdate(update);

                return Results.Ok();
            })
.WithName("TelegramWebhook");

            app.Run();
        }
    }
}
