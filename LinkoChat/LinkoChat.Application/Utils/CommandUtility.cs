using LinkoChat.Domain.Enums;
using System;
using System.Collections.Generic;

namespace LinkoChat.Application.Utils
{
    public static class CommandUtility
    {
        private static readonly Dictionary<string, Command> commandMappings = new Dictionary<string, Command>
        {
            { Constants.KeyboardButtons.CONNECT_RANDOM, Command.Connect },
            { Constants.KeyboardButtons.NEAR_PEOPLE, Command.NearPeople },
            { Constants.KeyboardButtons.SEARCH_PEOPLE, Command.SearchPeople },
            { Constants.KeyboardButtons.HELP, Command.Help },
            { Constants.KeyboardButtons.PROFILE, Command.Profile },
            { Constants.KeyboardButtons.COIN, Command.Coin },
            { Constants.KeyboardButtons.INVITE, Command.InviteFriends },
            { Constants.KeyboardButtons.REQUEST_USER_PROFILE, Command.InChatProfile },
            { Constants.KeyboardButtons.ENABLE_PRIVTE_CHAT, Command.InChatPrivate },
            { Constants.KeyboardButtons.END_CHAT, Command.InChatEnd }
        };

        private static readonly Dictionary<string, CommandType> callbackCommandMappings = new Dictionary<string, CommandType>
        {
            { "RandomSearch", CommandType.RandomSearch },
            { "CompleteProfile", CommandType.CompleteProfile },
            { "CancelSearch", CommandType.CancelSearch }
        };

        private static readonly Dictionary<string, Gender> genderCallbackMappings = new Dictionary<string, Gender>
        {
            { "All", Gender.Unknown },
            { "Boy", Gender.Boy },
            { "Girl", Gender.Girl }
        };

        public static bool IsCommand(string text, out Command command)
        {
            if (text.StartsWith("/start"))
            {
                command = Command.Start;
                return true;
            }

            if (commandMappings.TryGetValue(text, out command))
            {
                return true;
            }

            command = Command.Start;
            return false;
        }

        public static CommandType GetCallbackCommandType(string text)
        {
            return callbackCommandMappings[text];
        }
        public static Gender GetGenderTypeCallback(string text)
        {
            return genderCallbackMappings[text];
        }
    }
}
