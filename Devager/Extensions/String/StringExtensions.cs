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

            int distance =
                source.ToCharArray()
                .Zip(target.ToCharArray(), (char1, char2) => new { char1, char2 })
                .Count(m => m.char1 != m.char2);

            return distance;
        }
    }
}
