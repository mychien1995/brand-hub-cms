using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BrandHub.Utilities.Helpers
{
    public class EncryptUtils
    {
        public static string SHA256Encrypt(string phrase, string salt)
        {
            string saltAndPwd = String.Concat(phrase, salt);
            UTF8Encoding encoder = new UTF8Encoding();
            string hashedPwd = string.Empty;
            using (SHA256Managed sha256hasher = new SHA256Managed())
            {
                byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(saltAndPwd));
                hashedPwd = String.Concat(byteArrayToString(hashedDataBytes), salt);
            }
            return hashedPwd;
        }
        public static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }
        public static string CreateSalt(string UserName)
        {
            string username = UserName;
            byte[] userBytes;
            string salt;
            userBytes = ASCIIEncoding.ASCII.GetBytes(username);
            long XORED = 0x00;

            foreach (int x in userBytes)
                XORED = XORED ^ x;

            Random rand = new Random(Convert.ToInt32(XORED));
            salt = rand.Next().ToString();
            salt += rand.Next().ToString();
            salt += rand.Next().ToString();
            salt += rand.Next().ToString();
            return salt;
        }

        public static string GenerateAccessToken()
        {
            string source = Guid.NewGuid().ToString();
            string hash;
            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, source);
            }
            return Base64Encode(hash);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
