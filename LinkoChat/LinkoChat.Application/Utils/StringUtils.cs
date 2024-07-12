using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Application.Utils
{
    public static class StringUtils
    {
        private const string CHARACTERS = "abcdefghijklmnopqrstuvwxyz123456789";
        private static readonly Random rand = new Random();
        public static string GenerateUsername(int length = 6)
        {
            string result = string.Empty;
            for(int i = 0; i < length;i++)
                result += CHARACTERS[rand.Next(0, CHARACTERS.Length - 1)];

            return result;
        }
        public static bool IsInvited(string text, out string userName)
        {
            if (text.StartsWith("/start") && text.Contains(' '))
            {
                userName = text.Split(' ')[1];
                return true;
            }
            userName = null;
            return false;
        }
    }
}
