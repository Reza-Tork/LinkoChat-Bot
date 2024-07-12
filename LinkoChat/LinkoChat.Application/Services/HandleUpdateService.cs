using LinkoChat.Application.Exceptions;
using LinkoChat.Application.Interfaces;
using LinkoChat.Application.Utils;
using LinkoChat.Domain.Enums;
using LinkoChat.Domain.Models;
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
        private readonly IUserService _userService;
        private readonly IStateService _stateService;
        private readonly IChatService _chatService;

        public HandleUpdateService(ITelegramBotClient botClient, IUserService userService, IStateService stateService, IChatService chatService)
        {
            _botClient = botClient;
            _userService = userService;
            _stateService = stateService;
            _chatService = chatService;
        }

        public async Task HandleUpdate(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    await HandleMessage(update.Message);
                    break;
                case UpdateType.CallbackQuery:
                    await HandleCallbackQuery(update.CallbackQuery);
                    break;
            }

            await _userService.SaveChanges();
        }

        private async Task HandleMessage(Telegram.Bot.Types.Message message)
        {
            var user = message.From;
            var dbUser = await _userService.CheckUser(user.Id);

            switch (dbUser.StepStatus)
            {
                case StepStatus.Start:
                    await HandleStart(dbUser, message);
                    break;
                case StepStatus.ProfileGender:
                    await HandleProfileGender(dbUser, message);
                    break;
                case StepStatus.ProfileAge:
                    await HandleProfileAge(dbUser, message);
                    break;
                case StepStatus.ProfileState:
                    await HandleProfileState(dbUser, message);
                    break;
                case StepStatus.ProfileCity:
                    // Implement HandleProfileCity(dbUser, message);
                    break;
                case StepStatus.ProfileName:
                    // Implement HandleProfileName(dbUser, message);
                    break;
                case StepStatus.ProfilePicture:
                    // Implement HandleProfilePicture(dbUser, message);
                    break;
                case StepStatus.ProfileLocation:
                    // Implement HandleProfileLocation(dbUser, message);
                    break;
                case StepStatus.InChat:
                    await HandleInChat(dbUser, message);
                    break;
            }
        }

        private async Task HandleInChat(Domain.Models.User dbUser, Telegram.Bot.Types.Message message)
        {
            if(message.Type == MessageType.Text && CommandUtility.IsCommand(message.Text, out Command command))
            {
                switch (command)
                {
                    case Command.InChatEnd:
                        var currentChat = await _chatService.GetCurrentChat(dbUser.UserId);
                        var secondUser = await _userService.GetUser(currentChat.ConnectedTo);

                        await _chatService.EndChat(dbUser, currentChat);

                        dbUser.StepStatus = StepStatus.Start;
                        secondUser.StepStatus = StepStatus.Start;

                        await SendChatEnded(dbUser.UserId, secondUser.UserId, dbUser.Profile.Username, secondUser.Profile.Username);
                        break;
                    default:
                        await SendInChatError(message.Chat.Id);
                        break;
                }
                return;
            }

            var chat = await _chatService.GetCurrentChat(dbUser.UserId);
            switch (message.Type)
            {
                case MessageType.Text:
                    await _botClient.SendTextMessageAsync(chat.ConnectedTo, message.Text, entities: message.Entities);
                    break;
            }
        }

        private async Task HandleStart(Domain.Models.User dbUser, Telegram.Bot.Types.Message message)
        {
            if (message.Type != MessageType.Text)
            {
                await SendErrorMessage(message.Chat.Id);
                return;
            }

            if (CommandUtility.IsCommand(message.Text, out Command command))
            {
                switch (command)
                {
                    case Command.Start:
                        await HandleCommandStart(dbUser, message);
                        break;
                    case Command.Connect:
                        await SendConnectMessage(message.Chat.Id);
                        break;
                    case Command.Profile:
                        await SendProfileMessage(dbUser, message.Chat.Id);
                        break;
                }
            }
            else
            {
                await SendErrorMessage(message.Chat.Id);
            }
        }

        private async Task HandleCommandStart(Domain.Models.User dbUser, Telegram.Bot.Types.Message message)
        {
            if (!dbUser.IsRegistered)
            {
                if (dbUser.Profile.IsFirstStart)
                {
                    dbUser.Profile.IsFirstStart = false;
                    if (StringUtils.IsInvited(message.Text, out string callerUsername))
                    {
                        dbUser.Profile.CallerUsername = callerUsername;
                        var callerUser = await _userService.GetUser(callerUsername);
                        callerUser.Coin += 5;
                        await _botClient.SendTextMessageAsync(callerUser.UserId, string.Format(Constants.Texts.INVITER_COIN, 5, "دعوت کاربر", callerUser.Coin));
                    }
                }
                await SendGenderSelectionMessage(message.Chat.Id);
                dbUser.StepStatus = StepStatus.ProfileGender;
            }
            else
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, string.Format(Constants.Texts.HELLO_START, $"{message.From.FirstName} {message.From.LastName}"), replyMarkup: KeyboardUtils.MainKeyboard());
            }
        }

        private async Task HandleProfileGender(Domain.Models.User dbUser, Telegram.Bot.Types.Message message)
        {
            if (message.Text != Constants.Texts.GENDER_GIRL && message.Text != Constants.Texts.GENDER_BOY)
            {
                await SendGenderErrorMessage(message.Chat.Id);
                return;
            }

            dbUser.Profile.Gender = message.Text == Constants.Texts.GENDER_GIRL ? Gender.Girl : Gender.Boy;
            dbUser.Queue.Gender = dbUser.Profile.Gender;
            dbUser.StepStatus = StepStatus.ProfileAge;
            await SendAgeSelectionMessage(message.Chat.Id);
        }

        private async Task HandleProfileAge(Domain.Models.User dbUser, Telegram.Bot.Types.Message message)
        {
            if (int.TryParse(message.Text, out int age) && age >= 13 && age <= 100)
            {
                dbUser.Profile.Age = age;
                dbUser.StepStatus = StepStatus.ProfileState;
                await SendStateSelectionMessage(message.Chat.Id);
            }
            else
            {
                await SendAgeErrorMessage(message.Chat.Id);
            }
        }

        private async Task HandleProfileState(Domain.Models.User dbUser, Telegram.Bot.Types.Message message)
        {
            var state = await _stateService.GetState(message.Text);
            if (state != null)
            {
                dbUser.Location.State = state;
                dbUser.StepStatus = StepStatus.Start;
                dbUser.IsRegistered = true;
                await SendRegistrationCompleteMessage(message.Chat.Id);
            }
            else
            {
                await SendStateErrorMessage(message.Chat.Id);
            }
        }

        private async Task HandleCallbackQuery(CallbackQuery callback)
        {
            var dbUser = await _userService.GetUser(callback.From.Id);
            var commandType = CommandUtility.GetCallbackCommandType(callback.Data.Split(';')[0]);

            switch (commandType)
            {
                case CommandType.RandomSearch:
                    await HandleRandomSearch(callback, dbUser);
                    break;
                case CommandType.CancelSearch:
                    await HandleCancelSearch(callback, dbUser);
                    break;
            }
        }

        private async Task HandleRandomSearch(CallbackQuery callback, Domain.Models.User dbUser)
        {
            var gender = CommandUtility.GetGenderTypeCallback(callback.Data.Split(';')[1]);

            try
            {
                await _chatService.StartSearch(dbUser, gender);

                var message = await _botClient.EditMessageTextAsync(callback.Message.Chat.Id, callback.Message.MessageId, string.Format(Constants.MessagesText.SEARCHING_NOW, gender.ToFriendlyString()), replyMarkup: KeyboardUtils.CancelSearchKeyboard(), parseMode: ParseMode.Html);
                if (dbUser.StepStatus == StepStatus.InChat)
                {
                    var lastEnableChat = await _chatService.GetCurrentChat(dbUser.UserId);
                    await _botClient.DeleteMessageAsync(message.Chat.Id, message.MessageId);

                    await _botClient.SendTextMessageAsync(callback.Message.Chat.Id, @"👀 پیدا کردم وصلتون کردم

    به مخاطبت سلام کن 🗣", replyMarkup: KeyboardUtils.InChatKeyboard());

                    await _botClient.SendTextMessageAsync(lastEnableChat.ConnectedTo, @"👀 پیدا کردم وصلتون کردم

    به مخاطبت سلام کن 🗣", replyMarkup: KeyboardUtils.InChatKeyboard());
                }
            }
            catch (InChatException)
            {
                await SendInChatError(callback.Message.Chat.Id);
            }
            catch (Exception ex)
            {
                await _botClient.AnswerCallbackQueryAsync(callback.Id, ex.Message, true);
            }
        }
        private async Task HandleCancelSearch(CallbackQuery callback, Domain.Models.User dbUser)
        {
            try
            {
                await _chatService.StopSearch(dbUser.UserId);
                await _botClient.AnswerCallbackQueryAsync(callback.Id, "جستجو متوقف شد", true);
            }
            catch (Exception ex)
            {
                await _botClient.AnswerCallbackQueryAsync(callback.Id, ex.Message);
            }
            await _botClient.DeleteMessageAsync(callback.Message.Chat.Id, callback.Message.MessageId);
        }

        #region Error's Helper Methods
        private async Task SendChatEnded(long userId, long targetUserId,string username, string targetUsername)
        {
            await _botClient.SendTextMessageAsync(userId, $@"چت شما با /user_{targetUsername} توسط شما قطع شد

برای گزارش عدم رعایت قوانین (/help_terms) می توانید با لمس 《 🚫 گزارش کاربر 》 در پروفایل، کاربر را گزارش کنید.", replyMarkup: KeyboardUtils.MainKeyboard());

            await _botClient.SendTextMessageAsync(targetUserId, $@"چت شما با /user_{username} توسط کاربر مقابل قطع شد

برای گزارش عدم رعایت قوانین (/help_terms) می توانید با لمس 《 🚫 گزارش کاربر 》 در پروفایل، کاربر را گزارش کنید.", replyMarkup: KeyboardUtils.MainKeyboard());

        }
        private async Task SendInChatError(long chatId) =>
            await _botClient.SendTextMessageAsync(chatId, Constants.Texts.IN_CHAT_ERROR, parseMode: ParseMode.Html, replyMarkup: KeyboardUtils.InChatKeyboard());
        private async Task SendErrorMessage(long chatId) =>
            await _botClient.SendTextMessageAsync(chatId, Constants.Texts.ERROR, parseMode: ParseMode.Html);

        private async Task SendGenderSelectionMessage(long chatId) =>
            await _botClient.SendTextMessageAsync(chatId, Constants.Texts.GENDER_SELECT, parseMode: ParseMode.Html, replyMarkup: KeyboardUtils.GenderKeyboard());

        private async Task SendGenderErrorMessage(long chatId) =>
            await _botClient.SendTextMessageAsync(chatId, Constants.Texts.GENDER_ERROR, parseMode: ParseMode.Html, replyMarkup: KeyboardUtils.GenderKeyboard());

        private async Task SendAgeSelectionMessage(long chatId) =>
            await _botClient.SendTextMessageAsync(chatId, Constants.Texts.AGE_SELECT, parseMode: ParseMode.Html, replyMarkup: KeyboardUtils.AgeKeyboard());

        private async Task SendAgeErrorMessage(long chatId) =>
            await _botClient.SendTextMessageAsync(chatId, Constants.Texts.AGE_ERROR, parseMode: ParseMode.Html, replyMarkup: KeyboardUtils.AgeKeyboard());

        private async Task SendStateSelectionMessage(long chatId) =>
            await _botClient.SendTextMessageAsync(chatId, Constants.Texts.STATE_SELECT, parseMode: ParseMode.Html, replyMarkup: KeyboardUtils.StateKeyboard(await _stateService.GetAllStates()));

        private async Task SendStateErrorMessage(long chatId) =>
            await _botClient.SendTextMessageAsync(chatId, Constants.Texts.STATE_ERROR, parseMode: ParseMode.Html, replyMarkup: KeyboardUtils.StateKeyboard(await _stateService.GetAllStates()));

        private async Task SendRegistrationCompleteMessage(long chatId) =>
            await _botClient.SendTextMessageAsync(chatId, Constants.Texts.REGISTER_COMPLETE, parseMode: ParseMode.Html, replyMarkup: KeyboardUtils.MainKeyboard());

        private async Task SendConnectMessage(long chatId) =>
            await _botClient.SendTextMessageAsync(chatId, Constants.MessagesText.RANDOM_SEARCH, parseMode: ParseMode.Html, replyMarkup: KeyboardUtils.RandomSearchKeyboard());

        private async Task SendProfileMessage(Domain.Models.User dbUser, long chatId)
        {
            await _botClient.SendPhotoAsync(chatId, new InputFileId(dbUser.Profile.Picture ?? "AgACAgEAAxkBAANsZobZxuJ-ucjNahw5spQFIVPc8LIAAn6sMRuokDBEajhAITS4qTsBAAMCAAN5AAM1BA"),
                caption: string.Format(Constants.MessagesText.MYPROFILE, dbUser.Profile.Name, dbUser.Profile.Gender.ToFriendlyString(), dbUser.Location.State?.Name ?? "❓", dbUser.Location.City?.Name ?? "❓", dbUser.Profile.Age, 0, dbUser.Profile.Username));

            if (!dbUser.Profile.IsCompletedProfile)
            {
                await _botClient.SendTextMessageAsync(chatId, Constants.Texts.COMPLETE_PROFILE_REWARD, parseMode: ParseMode.Html, replyMarkup: KeyboardUtils.CompleteProfileMessageInlineKeyboard());
            }
        }
        #endregion
    }
}
