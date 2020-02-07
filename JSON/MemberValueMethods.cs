using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSON
{
    public static class MemberValueMethods
    {
        public static string Add_yyyy_MM(this string @string, DateTime dateTime) => @string + dateTime.ToString("_yyyy_MM");
    }
}
