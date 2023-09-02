using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WiresPrototype
{
    internal class Wires
    {
        public List<string> Colors { get; set; }
        public int Digit { get; set; }

        static List<string> acceptedColors = new List<string> { "red", "yellow", "blue", "white", "black" };
        static string pattern = $"\\b({string.Join("|", acceptedColors.Select(Regex.Escape))})\\b";

        static List<string> acceptedNumbers = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        static string numberPattern = $"\\b({string.Join("|", acceptedNumbers.Select(Regex.Escape))})\\b";

        public Wires(string speech)
        {
            Console.WriteLine(speech);

            List<string> colors = new List<string>();

            RegexOptions options = RegexOptions.IgnoreCase;

            MatchCollection colorMatches = Regex.Matches(speech, pattern, options);
            foreach (Match match in colorMatches)
            {
                colors.Add(match.Value.ToLower());
            }

            Match numberMatch = Regex.Match(speech, numberPattern, options);

            foreach (string s in colors)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine(numberMatch.Value);

            Digit = acceptedNumbers.IndexOf(numberMatch.Value);
            Colors = colors;
        }
    }
}
