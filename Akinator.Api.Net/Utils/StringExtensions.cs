using System;
using System.Net;

namespace Akinator.Api.Net.Utils
{
    public static class StringExtensions
    {
        public static string UrlEncode(this string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var temp = WebUtility.UrlEncode(input).ToCharArray();
            for (var i = 0; i < temp.Length - 2; i++)
            {
                if (temp[i] == '%')
                {
                    temp[i + 1] = char.ToUpper(temp[i + 1]);
                    temp[i + 2] = char.ToUpper(temp[i + 2]);
                }
            }
            return new string(temp);
       }
    }
}