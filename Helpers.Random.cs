using System;
using System.Linq;
using System.Threading;

namespace tp_unikit.Helpers.Randomness
{
    public static class RandomHelper
    {

        public static int s_seed = Environment.TickCount;

        private static ThreadLocal<Random> s_random = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref s_seed)));

        public static string RandomString(int amount) {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] stringChars = new char[amount + 1];
            for (int i = 0; i <= stringChars.Length - 1; i++) {
                stringChars[i] = chars[Random(0, chars.Length)];
            }
            var finalString = new string(stringChars);
            return finalString;
        }

        public static int RandomInteger(int amount) {
            string chars = "1234567890";
            char[] stringChars = new char[amount + 1];
            for (int i = 0; i <= stringChars.Length - 1; i++) {
                stringChars[i] = chars[Random(0, chars.Length)];
            }
            var finalString = new string(stringChars);
            return Convert.ToInt32(finalString);
        }

        public static int Random(int? min, int? max) {
            return Random(min == null ? 0 : (int)min, max == null ? 0 : (int)max);
        }

        public static int Random(int min, int max) {
            if (min == max || min > max) {
                return min;
            }
            return s_random.Value.Next(min, max);
        }

        public static int Random(int max) {
            return Random(0, max);
        }

        public static string NewGuid() {
            return Guid.NewGuid().ToString();
        }

        private static string GetRandomMacAddress() {
            var random = new Random();
            var buffer = new byte[6];
            random.NextBytes(buffer);
            var result = String.Concat(buffer.Select(x => string.Format("{0}:", x.ToString("X2"))).ToArray());
            return result.TrimEnd(':');
        }
    }
}
