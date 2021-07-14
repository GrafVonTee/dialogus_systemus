using System;

namespace DialogusSystemus
{
    static class StringExtensions
    {
        public static string Reverse(this String str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
