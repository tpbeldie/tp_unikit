using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace tp_unikit.Helpers.Cryptography
{
    public static class Helpers
    {

        public static string ByteToString(byte[] buff) {
            return buff.Aggregate(string.Empty, (current, item) => current + item.ToString("X2"));
        }

        public static string Base64Encode(string plainText) {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData) {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string CalculateMd5(string message) {
            var e = Encoding.UTF8;
            using (var m = MD5.Create()) {
                var hashed = m.ComputeHash(e.GetBytes(message));
                var hash = BitConverter.ToString(hashed).Replace("-", string.Empty).ToLower();
                return hash;
            }
        }

        public static string CalculateHash(string key, string message) {
            var e = Encoding.UTF8;
            const int HashBlockSize = 64;
            var keyBytes = e.GetBytes(key);
            var opadKeySet = new byte[64];
            var ipadKeySet = new byte[64];
            if (keyBytes.Length > HashBlockSize) {
                keyBytes = GetHash(keyBytes);
            }
            if (keyBytes.Length < HashBlockSize) {
                var newKeyBytes = new byte[64];
                keyBytes.CopyTo(newKeyBytes, 0);
                keyBytes = newKeyBytes;
            }
            for (var i = 0; i <= keyBytes.Length - 1; i++) {
                opadKeySet[i] = Convert.ToByte((keyBytes[i] ^ 0x5));
                ipadKeySet[i] = Convert.ToByte((keyBytes[i] ^ 0x36));
            }
            var hash = GetHash(ByteConcat(opadKeySet, GetHash(ByteConcat(ipadKeySet, e.GetBytes(message)))));
            return hash.Select(a => a.ToString("x2")).Aggregate((a, b) => $"{a}{b}");
        }

        public static byte[] GetHash(byte[] bytes) {
            using (var hash = SHA256.Create()) {
                return hash.ComputeHash(bytes);
            }
        }

        public static byte[] ByteConcat(byte[] left, byte[] right) {
            if (left == null) {
                return right;
            }
            if (right == null) {
                return left;
            }
            var newBytes = new byte[left.Length + right.Length - 1 + 1];
            left.CopyTo(newBytes, 0);
            right.CopyTo(newBytes, left.Length);
            return newBytes;
        }
    }
}
