#region

using System;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace FaceLook.Security.Concrete
{
    internal class Md5CryptoProvider
    {
        private const string Key = "admin1234";

        public string GetMd5Hash(string input)
        {
            MD5CryptoServiceProvider md5Crypto = new MD5CryptoServiceProvider();
            byte[] hashCode = md5Crypto.ComputeHash(Encoding.Default.GetBytes(input + Key));
            return BitConverter.ToString(hashCode).ToLower().Replace("-", "");
        }
    }
}