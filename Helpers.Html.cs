using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace tp_unikit.Helpers.Html
{
    public static class HtmlHelper
    {

        private static readonly Regex s_htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        public static string StripTagsRegex(string source) {
            return s_htmlRegex.Replace(source, string.Empty);
        }

        public static HtmlNode BuildDom(string content) {
            var dom = new HtmlDocument();
            dom.LoadHtml(content);
            return dom.DocumentNode;
        }

        public static string BuildPayload(IEnumerable<HtmlNode> inputNodes, string additionKeyValuePair = default) {
            var inputs = new List<string>(inputNodes.Count());
            foreach (HtmlNode node in inputNodes) {
                if (node.GetAttributeValue("type", string.Empty) != "hidden") {
                    continue;
                }
                var name = node.GetAttributeValue("name", null);
                var value = node.GetAttributeValue("value", null);
                if(string.IsNullOrEmpty(name)) {
                    continue;
                }
                inputs.Add($"{name}={value}");
            }
            if (!string.IsNullOrWhiteSpace(additionKeyValuePair)) {
                inputs.Add(additionKeyValuePair);
            }
           
            return string.Join("&", inputs);
        }

        public static string StripTagsCharArray(string source) {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;
            for (int i = 0; i < source.Length; i++) {
                char let = source[i];
                if (let == '<') {
                    inside = true;
                    continue;
                }
                if (let == '>') {
                    inside = false;
                    continue;
                }
                if (!inside) {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
}
