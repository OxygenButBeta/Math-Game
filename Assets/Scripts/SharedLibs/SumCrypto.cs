using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SharedLibs
{
    public static class SumCrypto
    {
        private const string key = "?'Qan4%&74U12345";
        public static string DecryptString(string cipherText) {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16];

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText));
            using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }

    }
}

