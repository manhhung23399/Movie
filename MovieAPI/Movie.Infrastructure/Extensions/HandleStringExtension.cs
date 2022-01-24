using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movie.Infrastructure.Extensions
{
    public static class HandleStringExtension
    {
        public static List<string> SplitStringToList(this string input, string character)
        {
            return input.Contains(character) ? input.Split(character).ToList() : new List<string> { character };
        }
    }
}
