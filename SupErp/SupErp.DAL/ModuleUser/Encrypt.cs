using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.DAL.ModuleUser
{
    public static class Encrypt
    {
        public static string hashSHA256(string unhashedValue)
        {
            SHA256 shaM = new SHA256Managed();
            byte[] hash = shaM.ComputeHash(Encoding.ASCII.GetBytes(unhashedValue));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }
    }
}
