namespace Devager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Maths
    {
        public static float EgimHesabi(List<float> y, List<float> x)
        {
            // x ve y nin count sayısı aylı kontrolu yapılabilir...
            var yo = y.Average();
            var xo = x.Average();

            var h = new List<float>();
            var f = new List<float>();

            for (int i = 0; i < x.Count; i++)
            {
                h.Add((float)(x[i] - xo) * (y[i] - yo));
                f.Add((float)Math.Pow((x[i] - xo), 2));
            }

            return h.Sum() / f.Sum();
        }
    }
}
