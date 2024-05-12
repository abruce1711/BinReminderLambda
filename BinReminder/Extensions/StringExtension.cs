using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Extensions
{
    public static class StringExtension
    {
        public static string Embolden(this string text)
        {
            return "*" + text + "*";
        }
    }
}
