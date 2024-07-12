using LinkoChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace LinkoChat.Application.Utils
{
    public static class KeyboardUtils
    {
        public static IReplyMarkup GenderKeyboard()
        {
            return new ReplyKeyboardMarkup(new List<KeyboardButton>()
            {
                new(Constants.Texts.GENDER_BOY),
                new(Constants.Texts.GENDER_GIRL),
            })
            {
                ResizeKeyboard = true
            };
        }

        public static IReplyMarkup AgeKeyboard()
        {
            var finalKeyboard = new List<List<KeyboardButton>>();
            for (int i = 0; i < 22; i++)
            {
                var keyboardRow = new List<KeyboardButton>();
                for (int j = 0; j < 4; j++)
                {
                    var buttonText = (13 + (j + (i * 4)));
                    keyboardRow.Add(new KeyboardButton(buttonText.ToString()));
                }
                finalKeyboard.Add(keyboardRow);
            }
            return new ReplyKeyboardMarkup(finalKeyboard)
            {
                ResizeKeyboard = true
            };
        }

        public static IReplyMarkup StateKeyboard(IEnumerable<State> states)
        {
            var keyboardButtons = new List<KeyboardButton>();

            foreach (var state in states)
            {
                keyboardButtons.Add(new KeyboardButton(state.Name));
            }

            var keyboard = new List<KeyboardButton[]>();

            for (int i = 0; i < keyboardButtons.Count; i += 3)
            {
                keyboard.Add(keyboardButtons.Skip(i).Take(3).ToArray());
            }

            return new ReplyKeyboardMarkup(keyboard)
            {
                ResizeKeyboard = true
            };
        }

        public static IReplyMarkup MainKeyboard()
        {
            var keyboardButtons = new List<List<KeyboardButton>>()
            {
                new()
                {
                    new(Constants.KeyboardButtons.CONNECT_RANDOM)
                },
                new()
                {
                    new(Constants.KeyboardButtons.NEAR_PEOPLE),
                    new(Constants.KeyboardButtons.SEARCH_PEOPLE)
                },
                new()
                {
                    new(Constants.KeyboardButtons.HELP),
                    new(Constants.KeyboardButtons.PROFILE),
                    new(Constants.KeyboardButtons.COIN)
                },
                new()
                {
                    new(Constants.KeyboardButtons.INVITE)
                }
            };
            return new ReplyKeyboardMarkup(keyboardButtons)
            {
                ResizeKeyboard = true
            };
        }

        public static IReplyMarkup CompleteProfileMessageInlineKeyboard()
        {
            return new InlineKeyboardMarkup(new List<InlineKeyboardButton>()
            {
                new(Constants.InlineKeyboardButtons.COMPLETE_PROFILE)
                {
                    CallbackData = "CompleteProfile;"
                }
            });
        }

        public static IReplyMarkup RandomSearchKeyboard()
        {
            return new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>()
            {
                new List<InlineKeyboardButton>()
                {
                    new(Constants.InlineKeyboardButtons.RANDOM_SEARCH)
                    {
                        CallbackData = "RandomSearch;All"
                    }
                },
                new List<InlineKeyboardButton>()
                {
                    new(Constants.InlineKeyboardButtons.RANDOM_SEARCH_BOY)
                    {
                        CallbackData = "RandomSearch;Boy"
                    },
                    new(Constants.InlineKeyboardButtons.RANDOM_SEARCH_GIRL)
                    {
                        CallbackData = "RandomSearch;Girl"
                    }
                },
            });
        }
        public static InlineKeyboardMarkup CancelSearchKeyboard()
        {
            return new InlineKeyboardMarkup(new InlineKeyboardButton(Constants.InlineKeyboardButtons.CANCEL_SEARCH)
            {
                CallbackData = "CancelSearch;"
            });
        }

        public static ReplyKeyboardMarkup InChatKeyboard()
        {
            var keyboardButtons = new List<List<KeyboardButton>>()
            {
                new()
                {
                    new(Constants.KeyboardButtons.REQUEST_USER_PROFILE)
                },
                new()
                {
                    new(Constants.KeyboardButtons.ENABLE_PRIVTE_CHAT)
                },
                new()
                {
                    new(Constants.KeyboardButtons.END_CHAT),
                }
            };
            return new ReplyKeyboardMarkup(keyboardButtons)
            {
                ResizeKeyboard = true
            };
        }
    }
}
