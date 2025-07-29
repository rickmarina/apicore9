using System.Security.Cryptography;
using System.Text;

namespace com.rorisoft.utils
{
    public class EncryptionUtils
    {
        
        public static string getSHA256(string cad)
        {
            using var sha = SHA256.Create();
            byte[] bytesToHash = Encoding.ASCII.GetBytes(cad);
            byte[] hash = sha.ComputeHash(bytesToHash);
            var resultado = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
                resultado.Append(b.ToString("x2"));
            return resultado.ToString();
        }

        public static string md5(string value)
        {
            using var md5 = MD5.Create();
            byte[] data = Encoding.ASCII.GetBytes(value);
            byte[] hash = md5.ComputeHash(data);
            var ret = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
                ret.Append(b.ToString("x2"));
            return ret.ToString();
        }
    }
}
