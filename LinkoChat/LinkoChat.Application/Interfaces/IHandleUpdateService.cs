using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace LinkoChat.Application.Interfaces
{
    public interface IHandleUpdateService
    {
        Task HandleUpdate(Update update);
    }
}
