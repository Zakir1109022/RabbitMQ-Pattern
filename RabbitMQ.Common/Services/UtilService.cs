using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Common.Services
{
   public static class UtilService
    {

        public static string key = "@@5z4a3k@@zakir";

        public static string ConvertToEncript(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";

            password += key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);

        }

        public static string ConvertToDecript(string base64EncodeData)
        {
            if (string.IsNullOrEmpty(base64EncodeData)) return "";
            var base64EncodedBytes = Convert.FromBase64String(base64EncodeData);
            var result = Encoding.UTF8.GetString(base64EncodedBytes);
            result = result.Substring(0, result.Length - key.Length);
            return result;
        }
    }
}
