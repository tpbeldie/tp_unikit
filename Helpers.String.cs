using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace tp_unikit.Helpers.Strings
{
    public static class StringHelper
    {

        public enum WordCase
        {
            /* :: All characters are upper case. :: */
            Upper,
            /* :: All characters are lower case. :: */
            Lower,
            /* :: Only the first character is upper case. :: */
            Capital,
            /* :: Unknown patternal mixture of upper and lower cases. :: */
            UnknownMixture
        }

        public static WordCase GetWordCase(this string word) {
            if (string.IsNullOrEmpty(word) || word.All(@char => char.IsDigit(@char))) {
                return WordCase.UnknownMixture;
            }
            if (word.All(@char => char.IsUpper(@char))) {
                return WordCase.Upper;
            }
            if (word.All(@char => char.IsLower(@char))) {
                return WordCase.Lower;
            }
            if (char.IsUpper(word.FirstOrDefault())) {
                return WordCase.Capital;
            }
            return WordCase.UnknownMixture;
        }

        public static string SetWordCase(this string word, WordCase wordCase) {
            switch (wordCase) {
                case WordCase.Upper: {
                        return word.ToUpper();
                    }
                case WordCase.Lower: {
                        return word.ToLower();
                    }
                case WordCase.Capital: {
                        word = word.ToLower();
                        var chars = word.ToCharArray();
                        chars[0] = Char.ToUpperInvariant(chars[0]);
                        return new String(chars);
                    }
                default: {
                        return word;
                    }
            }
        }

        public static string Substring(this string input, int amount = 1) {
            if (input.Length > amount) {
                input = input.Remove(input.Length - amount);
            }
            return input;
        }

        public static string GetFormattedMacAddress(this string macAddress) {
            return (macAddress.Length != 12) ? "00:00:00:00:00:00" : Regex.Replace(macAddress, "(.{2})(.{2})(.{2})(.{2})(.{2})(.{2})", $"1:$2:$3:$4:$5:$6");
        }

        public static List<string> GetBetweenAll(this string data, string start, string end) {
            List<string> matched = new List<string>();
            bool exit = false;
            while (!exit) {
                int indexStart = data.IndexOf(start);
                if (indexStart != -1) {
                    int indexEnd = indexStart + data.Substring(indexStart).IndexOf(end);
                    matched.Add(data.Substring(indexStart + start.Length, indexEnd - indexStart - start.Length));
                    data = data.Substring(indexEnd + end.Length);
                }
                else {
                    exit = true;
                }
            }
            return matched;
        }

        public static List<string> RegexGetBetweenAll(this string source, string startPoint, string endPoint) {
            List<string> results = new List<string>(), t = new List<string>();
            try {
                t.AddRange(Regex.Split(source, startPoint));
                t.RemoveAt(0);
                foreach (string s in t) {
                    results.Add(Regex.Split(s, endPoint)?[0]);
                }
                t.Clear();
            }
            catch { }
            return results;
        }

        public static string RegexGetBetween(this string source, string startPoint, string endPoint) {
            return RegexGetBetweenAll(source, startPoint, endPoint)?[0];
        }

        public static string GetBetween(this string data, string start, string end) {
            return GetBetweenAll(data, start, end)?[0];
        }

        public static IEnumerable<string> SplitElement(this string source, string delim) {
            int newIndex;
            int oldIndex = 0;
            while ((newIndex = source.IndexOf(delim, oldIndex)) != -1) {
                yield return source.Substring(oldIndex, newIndex - oldIndex);
                oldIndex = newIndex + delim.Length;
            }
            yield return source.Substring(oldIndex);
        }

        public static string[] Split(this string input, string separator, StringSplitOptions stringSplitOptions) {
            return input.Split(new[] { separator }, stringSplitOptions);
        }

        /* 
         * :: Check whether a string contains a particular query more than a specific amount of times. ::
         */
        public static bool ContainsMoreThan(this string text, int count, string value, StringComparison comparison = StringComparison.CurrentCulture) {
            if (string.IsNullOrEmpty(text)) {
                return false;
            }
            if (string.IsNullOrEmpty(value)) {
                return false;
            }
            int contains = 0;
            int index = 0;
            while ((index = text.IndexOf(value, index, text.Length - index, comparison)) != -1) {
                if (Interlocked.Increment(ref contains) > count) {
                    return true;
                }
                index += 1;
            }
            return false;
        }

        public static int GetNumeric(this string input) {
            var digits = string.Concat(input.Where(c => char.IsDigit(c)));
            if (input == null || digits == null) {
                return 0;
            }
            return System.Convert.ToInt32(digits);
        }

        public static bool ContainsAny(this string input, IEnumerable<string> containsKeywords, StringComparison comparisonType) {
            return containsKeywords.Any(keyword => input.IndexOf(keyword, comparisonType) >= 0);
        }

        public static bool ContainsAny(this string input, IEnumerable<string> containsKeywords) {
            return containsKeywords.Any(keyword => input.IndexOf(keyword) >= 0);
        }

        public static bool ContainsAll(this string input, params string[] needles) {
            var list = needles.ToList();
            return list.All(keyword => input.IndexOf(keyword) >= 0);
        }

        public static bool EqualsAny(this string haystack, params string[] needles) {
            foreach (string needle in needles) {
                if (haystack == needle) {
                    return true;
                }
            }
            return false;
        }

        public static bool ContainsAny(this string haystack, params string[] needles) {
            foreach (string needle in needles) {
                if (haystack.Contains(needle)) {
                    return true;
                }
            }
            return false;
        }

        public static bool Contains(string[] target, string value) {
            return target.Any(i => i.Contains(value));
        }

        public static string Encode(this string content) {
            return "\"" + content + "\"";
        }

    }
}
