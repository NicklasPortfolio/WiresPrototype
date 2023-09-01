using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WiresPrototype
{
    internal class Wires
    {
        public List<string> Colors { get; set; }
        public int Digit { get; set; }

        static List<string> acceptedColors = new List<string> { "red", "yellow", "blue", "white", "black" };
        static string pattern = $"\\b({string.Join("|", acceptedColors.Select(Regex.Escape))})\\b";

        static List<string> acceptedNumbers = new List<string> { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        static string numberPattern = $"\\b({string.Join("|", acceptedNumbers.Select(Regex.Escape))})\\b";

        public Wires(string speech)
        {
            List<string> colors = new List<string>();

            MatchCollection colorMatches = Regex.Matches(speech, pattern);
            foreach (Match match in colorMatches)
            {
                colors.Add(match.Value.ToLower());
            }

            Match numberMatch = Regex.Match(speech, numberPattern);

            Digit = acceptedNumbers.IndexOf(numberMatch.Value);
            Colors = colors;
        }
    }
}
