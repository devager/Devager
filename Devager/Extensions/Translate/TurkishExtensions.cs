namespace Devager.Extensions.Translate
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Web;
    public static class TurkishExtensions
    {
        public static string AddPossessiveSuffix(this string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return "";

            var tmp = word;

            // Sayıları okunuşlarıyla değiştir:
            tmp = tmp.Replace("0", "sıfır");
            tmp = tmp.Replace("1", "bir");
            tmp = tmp.Replace("2", "iki");
            tmp = tmp.Replace("3", "üç");
            tmp = tmp.Replace("4", "dört");
            tmp = tmp.Replace("5", "beş");
            tmp = tmp.Replace("6", "altı");
            tmp = tmp.Replace("7", "yedi");
            tmp = tmp.Replace("8", "sekiz");
            tmp = tmp.Replace("9", "dokuz");

            var lastVowelIndex = -1;

            for (var i = 0; i < tmp.Length; i++)
            {
                if ("aeıioöuü".Contains(tmp[i]))
                {
                    lastVowelIndex = i;
                }
            }
            var sb = new StringBuilder();
            //string toplama çok YAVAŞ diyorlar
            //var suffix = "";

            var bufferLetter = String.Empty; // kaynaştırma harfi

            var lastVowel = lastVowelIndex > -1 ? tmp[lastVowelIndex] : 'ı'; // Örn. trt'ın

            if (tmp.Length == lastVowelIndex + 1) // Sesli harfle bitiyorsa
                bufferLetter = "n";

            //suffix += "'" + bufferLetter;
            sb.Append("'");
            sb.Append(bufferLetter);

            if ("aı".Contains(lastVowel))
                //suffix += "ın";
                sb.Append("ın");
            else if ("ei1".Contains(lastVowel))
                sb.Append("in");
            //suffix += "in";
            else if ("ou".Contains(lastVowel))
                sb.Append("un");
            //suffix += "un";
            else if ("öü".Contains(lastVowel))
                sb.Append("ün");
            //suffix += "ün";

            return word + sb.ToString();
        }

        public static string TcIdGenerator()
        {
            var a = GetRandomNumber();
            var x = a.ToCharArray();
            var b = new int[10];
            for (var i = 0; i < x.Length; i++)
            {
                b[i] = Convert.ToInt32(x.GetValue(i).ToString(), 10);
            }

            var c = b[0] + b[2] + b[4] + b[6] + b[8];
            var d = b[1] + b[3] + b[5] + b[7];
            var e = (7 * c - d) % 10;

            var result = a + ("" + e) + ("" + (d + c + e) % 10);

            if (!ValidTurkishIdNo(result))
                TcIdGenerator();

            return result;

        }

        private static string GetRandomNumber()
        {
            var rnd = new Random();
            var result = "" + Math.Floor(900000001 * rnd.Next() + 1e8);

            int n;
            var isNumeric = int.TryParse(result, out n);
            if (!isNumeric || result.Substring(0, 1) == "-")
                GetRandomNumber();

            return result;
        }

        public static bool ValidTurkishIdNo(this string tcNo)
        {
            var tc = new int[11];
            for (var i = 0; i < 11; i++)
            {
                string a = tcNo[i].ToString();
                tc[i] = Convert.ToInt32(a);
            }

            int singurals = 0;
            int plurals = 0;

            for (int k = 0; k < 9; k++)
            {
                if (k % 2 == 0)
                    singurals += tc[k];
                else if (k % 2 != 0)
                    plurals += tc[k];
            }

            int t1 = (singurals * 3) + plurals;
            int c1 = (10 - (t1 % 10)) % 10;
            int t2 = c1 + plurals;
            int t3 = (t2 * 3) + singurals;
            int c2 = (10 - (t3 % 10)) % 10;

            if (c1 == tc[9] && c2 == tc[10])
                return true;
            else
                return false;
        }

        public static bool ContainsTurkishChar(this string target)
        {
            var trCharList = "ğüşıöçĞÜŞİÖÇ".ToList();
            return target.Any(trCharList.Contains);
        }
    }
}
