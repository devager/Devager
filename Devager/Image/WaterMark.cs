namespace Devager
{
    using System.Drawing;
    using System.Windows.Forms;

    public static class WaterMark
    {
        public static Image Create(this Image source, Image watermark)
        {
            try
            {
                var wm = new Watermarker(source)
                {
                    Margin = new Padding(0, 0, 15, 5),
                    Position = WatermarkPosition.BottomRight,
                    TransparentColor = Color.Transparent
                };
                wm.DrawImage(watermark);
                return wm.Image;
            }
            catch
            {
                return source;
            }
        }

    }
}


