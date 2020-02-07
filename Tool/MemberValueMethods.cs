using System;

namespace JSON
{
    public static class MemberValueMethods
    {
        public static string Add_yyyy_MM(this string @string, DateTime dateTime) => @string + dateTime.ToString("_yyyy_MM");
    }
}
