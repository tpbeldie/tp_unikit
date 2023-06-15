using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace tp_unikit.Helpers.RegularExpression
{
    public static class RegexHelper
    {
        public static Match GetMatch(string source, Regex expression) {
            return expression.Match(source);
        }

        public static MatchCollection GetMatches(string source, Regex expression) {
            return expression.Matches(source);
        }

        public static string RegexParse(string str, string pattern, int matchCollectionIndex = 0, int GroupIndex = 1) {
            MatchCollection matches = Regex.Matches(str, pattern);
            return matches[matchCollectionIndex].Groups[GroupIndex]?.Value;
        }

        public static string RegexParse(string source, string pattern) {
            MatchCollection matches = Regex.Matches(source, pattern);
            return matches[0]?.Groups[1]?.Value;
        }

        public static string RegexParse(string source, string pattern, int matchCollectionIndex = 0) {
            MatchCollection matches = Regex.Matches(source, pattern);
            return matches[matchCollectionIndex]?.Groups[1]?.Value;
        }

        public static string StraightRegex(string source, string pattern, int straightId = 1) {
            var match = Regex.Match(source, pattern);
            return match?.Groups[straightId]?.Value;
        }

        public static List<string> RegexArray(string source, string pattern) {
            var matches = Regex.Matches(source, pattern);
            List<string> list = new List<string>(matches.Count);
            foreach (Match matchs in matches) {
                list.Add(matchs?.Value);
            }
            return list;
        }

        public static string GetMatch(string source, string pattern) {
            MatchCollection enumerator = Regex.Matches(source, pattern);
            return enumerator[0].Groups[1]?.Value;
        }
    }
}
