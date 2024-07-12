using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Application.Exceptions
{
    public class InChatException : Exception
    {
        public InChatException(string? message, Exception? innerException) { }
        public InChatException(string? message) : base(message) { }
        public InChatException() { }
    }
}
