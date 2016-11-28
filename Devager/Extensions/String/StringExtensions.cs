namespace Devager.Extensions.String
{
    using System;
    using System.Linq;

    public static class StringExtensions
    {
        public static int CalculateHammingDistance(this string source, string target)
        {
            if (source.Length != target.Length)
                throw new Exception("Metinler eşit uzunlukta olmalı");

            var distance =
                source.ToCharArray()
                .Zip(target.ToCharArray(), (char1, char2) => new { char1, char2 })
                .Count(m => m.char1 != m.char2);

            return distance;
        }

        public static bool TcDogrula(this string tcKimlikNo)
        {
            var returnvalue = false;
            if (tcKimlikNo.Length == 11)
            {
                var tcNo = long.Parse(tcKimlikNo);

                var atcno = tcNo / 100;
                var btcno = tcNo / 100;

                var c1 = atcno % 10; atcno = atcno / 10;
                var c2 = atcno % 10; atcno = atcno / 10;
                var c3 = atcno % 10; atcno = atcno / 10;
                var c4 = atcno % 10; atcno = atcno / 10;
                var c5 = atcno % 10; atcno = atcno / 10;
                var c6 = atcno % 10; atcno = atcno / 10;
                var c7 = atcno % 10; atcno = atcno / 10;
                var c8 = atcno % 10; atcno = atcno / 10;
                var c9 = atcno % 10; atcno = atcno / 10;
                var q1 = ((10 - ((((c1 + c3 + c5 + c7 + c9) * 3) + (c2 + c4 + c6 + c8)) % 10)) % 10);
                var q2 = ((10 - (((((c2 + c4 + c6 + c8) + q1) * 3) + (c1 + c3 + c5 + c7 + c9)) % 10)) % 10);

                returnvalue = ((btcno * 100) + (q1 * 10) + q2 == tcNo);
            }
            return returnvalue;
        }
    }
}
