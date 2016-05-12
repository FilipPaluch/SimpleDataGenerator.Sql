using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Sql.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveBlanks(this string str)
        {
            return str.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");
        }
    }
}
