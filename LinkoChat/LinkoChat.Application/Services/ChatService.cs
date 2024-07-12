using LinkoChat.Application.Exceptions;
using LinkoChat.Application.Interfaces;
using LinkoChat.Domain.Enums;
using LinkoChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserService _userService;

        public ChatService(IChatRepository chatRepository, IUserService userService)
        {
            _chatRepository = chatRepository;
            _userService = userService;
        }
        public async Task<ICollection<Chat>> GetAllChats(long userId)
        {
            return await _chatRepository.GetChats(userId);
        }

        public async Task<Chat> GetCurrentChat(long userId)
        {
            return await _chatRepository.GetLastChat(userId);
        }

        public async Task SendMessage(long From, long To, int messageId)
        {
            var chat = await GetCurrentChat(From);
            await _chatRepository.AddMessage(new()
            {
                ChatId = chat.ChatId,
                From = From,
                To = To,
                MessageId = messageId
            });
        }

        public async Task StartSearch(User user, Gender gender)
        {
            if (user.Queue.IsSearching)
            {
                throw new Exception("درحال جستجو هستیم ، نزن دیگه عه");
            }
            else if (user.StepStatus == StepStatus.InChat)
            {
                throw new InChatException();
            }
            else if(gender != Gender.Unknown)
            {
                if(user.Coin < 2)
                {
                    throw new Exception($"سکه کافی برای جستجوی {gender.ToFriendlyString()} نداری");
                }
                user.Coin -= 2;
            }

            var queues = await _chatRepository.GetQueues();
            var queueWant = queues.OrderBy(x => x.SearchingFrom).FirstOrDefault(q =>
                q.UserId != user.UserId &&
                q.IsSearching &&
                (gender == Gender.Unknown
                    ? q.RequestedGender == Gender.Unknown || q.RequestedGender == Gender.Boy || q.RequestedGender == Gender.Girl
                    : q.RequestedGender == gender && q.RequestedGender == user.Profile.Gender));


            if (queueWant != null)
            {
                var qUser = await _userService.GetUser(queueWant.UserId);
                await StartChat(user, qUser);
            }
            else
            {
                user.Queue.IsSearching = true;
                user.Queue.RequestedGender = gender;
                user.Queue.SearchingFrom = DateTime.Now;
            }
        }

        public async Task StopSearch(long userId)
        {
            var queue = await GetCurrentSearch(userId);
            if (queue == null)
                throw new Exception("جستجوی فعالی وجود ندارد");
            _chatRepository.DeleteQueue(queue);
        }

        public async Task<Queue> GetCurrentSearch(long userId)
        {
            return await _chatRepository.GetQueue(userId);
        }

        public async Task EndChat(User user, Chat chat)
        {
            var secondUserChat = await GetCurrentChat(chat.ConnectedTo);

            chat.IsEnded = true;
            secondUserChat.IsEnded = true;
        }

        public async Task StartChat(User user, User targetUser)
        {
            user.StepStatus = StepStatus.InChat;
            targetUser.StepStatus = StepStatus.InChat;

            user.Queue.IsSearching = false;
            targetUser.Queue.IsSearching = false;

            await _chatRepository.AddChat(new Chat()
            {
                UserId = user.UserId,
                ConnectedTo = targetUser.UserId,
                IsEnded = false
            });

            await _chatRepository.AddChat(new Chat()
            {
                UserId = targetUser.UserId,
                ConnectedTo = user.UserId,
                IsEnded = false
            });
        }
    }
}
