using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Domain.Enums
{
    public enum Gender
    {
        Boy,
        Girl,
        Unknown
    }
    public static class GenderExtensions
    {
        public static string ToFriendlyString(this Gender me)
        {
            switch (me)
            {
                case Gender.Boy:
                    return "پسر🙎‍♂️";
                case Gender.Girl:
                    return "🙎‍♀️دختر";
                case Gender.Unknown:
                    return "❓";
                default:
                    return "❓";
            }
        }
    }
}
