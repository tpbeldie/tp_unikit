using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace tp_unikit.Helpers.UniformResourceIdentifier
{
    public static class UriHelper
    {
        public static Uri Append(this Uri uri, params string[] paths) {
            return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => string.Format("{0}/{1}", current.TrimEnd('/'), path.TrimStart('/'))));
        }

        public static Uri AddParameter(this Uri url, string paramName, string paramValue) {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue;
            uriBuilder.Query = query.ToString();
            return uriBuilder.Uri;
        }

        public static Uri Build(Uri defaultUri, string endPoint, JObject fields) {
            var queryBuilder = new StringBuilder();
            if (fields != null) {
                foreach (var obj in fields) {
                    queryBuilder.Append(String.Format("&{0}={1}", obj.Key, obj.Value));
                }
            }
            return Build(defaultUri, endPoint, ref queryBuilder);
        }

        public static Uri Build(Uri defaultUri, string endPoint, Dictionary<string, string> fields) {
            var queryBuilder = new StringBuilder();
            if (fields != null) {
                foreach (var obj in fields) {
                    queryBuilder.Append(String.Format("&{0}={1}", obj.Key, obj.Value));
                }
            }
            return Build(defaultUri, endPoint, ref queryBuilder);
        }

        private static Uri Build(Uri defaultUri, string endPoint, ref StringBuilder queries) {
            var uri = endPoint + queries.ToString();
            StringBuilder sb = new StringBuilder(uri);
            sb[uri.IndexOf('&')] = '?';
            uri = sb.ToString();
            return new Uri(defaultUri, uri);
        }

        public static Uri Build(Uri defaultUri, string endPoint) {
            return new Uri(defaultUri, endPoint);
        }

        public static Uri Build(Uri defaultUri, string endPoint, string queryData) {
            return Build(defaultUri, endPoint, queryData);
        }

        public static string AsQueryString(this Dictionary<string, string> parameters) {
            if (!parameters.Any()) { 
                return String.Empty; 
            }
            var builder = new StringBuilder("?");
            var separator = "";
            foreach (var kvp in parameters.Where(kvp => kvp.Value != null)) {
                builder.AppendFormat("{0}{1}={2}", separator, WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value));
                separator = "&";
            }
            return builder.ToString();
        }
    }
}
