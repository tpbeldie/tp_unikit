using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace tp_unikit.Helpers.IO
{
    public static class FileHelper
    {

        private static Encoding GetEncoding(Encoding encoding) {
            return encoding != null ? encoding : Encoding.UTF8;
        }

        public static string ReadFile(string filePath, Encoding encoding) {
            string data = string.Empty;
            if (File.Exists(filePath))
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                    using (StreamReader sr = new StreamReader(fs, GetEncoding(encoding))) {
                        data = sr.ReadToEnd();
                        sr.Close();
                    }
                }
            return data;
        }

        public static void RewriteFile(string filePath, string input, Encoding encoding = null) {
            using (StreamWriter sw = new StreamWriter(filePath, false, GetEncoding(encoding))) {
                sw.Write(input);
                sw.Close();
            }
        }

        public static void AppendToFile(string filePath, string input, Encoding encoding = null) {
            using (StreamWriter sw = new StreamWriter(filePath, true, GetEncoding(encoding))) {
                sw.Write(input);
                sw.Close();
            }
        }
        public static void CreateFile(string filePath, string input,   Encoding encoding = null) {
            using (StreamWriter sw = new StreamWriter(filePath, false, GetEncoding(encoding))) {
                sw.Write(input);
                sw.Close();
            }
        }

        public static IEnumerable<string> FileToList(string filePath) {
            if (File.Exists(filePath)) {
                string line = string.Empty;
                using (var reader = new StreamReader(filePath)) {
                    while ((line = reader.ReadLine()) != null) {
                        yield return line;
                    }
                }
            }
        }

        public static IEnumerable<string> ReadAndFilter(string filePath, Predicate<string> condition) {
            using (var reader = new StreamReader(filePath)) {
                string line = string.Empty;
                while ((line = reader.ReadLine()) != null) {
                    if (condition(line)) {
                        yield return line;
                    }
                }
            }
        }

        public static string FileLength(this string filePath, bool uppercase = false) {
            string[] sizes = new[] { "b", "kb", "mb", "gb", "tb" };
            double len = new FileInfo(filePath).Length;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1) {
                order += 1;
                len = len / 1024;
            }
            if (uppercase) {
                return string.Format("{0:0.##} {1}", len, sizes[order]);
            }
            else {
                return string.Format("{0:0.##} {1}", len, sizes[order]);
            }
        }
    }
}