namespace Devager
{
    using System.Security.Cryptography;
    using System.Text;
    public static class Encrypt
    {
        public static string GetMd5Sum(string str)
        {
            var enc = Encoding.Unicode.GetEncoder();
            var unicodeText = new byte[str.Length * 2];
            enc.GetBytes(str.ToCharArray(), 0, str.Length, unicodeText, 0, true);

            var md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(unicodeText);

            var sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
