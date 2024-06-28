using LinkoChat.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LinkoChat.Application.Services
{
    public class HandleUpdateService : IHandleUpdateService
    {
        private readonly ITelegramBotClient _botClient;

        public HandleUpdateService(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task HandleUpdate(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    await BotOnMessage(update);
                    break;

                case UpdateType.EditedMessage:
                    break;

                case UpdateType.CallbackQuery:
                    break;

                case UpdateType.Unknown:

                    break;
            }
        }


        public async Task BotOnMessage(Update update)
        {
            var message = update.Message;
            var user = update.Message.From;

            



        }
    }
}
