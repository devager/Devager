namespace Devager.Image
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    public enum WatermarkPosition
    {
        Absolute,
        TopLeft,
        TopRight,
        TopMiddle,
        BottomLeft,
        BottomRight,
        BottomMiddle,
        MiddleLeft,
        MiddleRight,
        Center
    }

    public class Watermarker
    {
        private Image _originalImage;
        private Image _watermark;

        public Watermarker(Image image)
        {
            Position = WatermarkPosition.Absolute;
            Opacity = 1.0f;
            TransparentColor = Color.Empty;
            RotateFlip = RotateFlipType.RotateNoneFlipNone;
            Margin = new Padding(0);
            ScaleRatio = 1.0f;
            TextFont = new Font(FontFamily.GenericSansSerif, 10);
            TextFontColor = Color.Black;

            LoadImage(image);
        }

        public Watermarker(string filename) : this(Image.FromFile(filename)) { }

        [Browsable(false)]
        public Image Image { get; private set; }

        public WatermarkPosition Position { get; set; }
        public int AbsolutePositionX { get; set; }
        public int AbsolutePositionY { get; set; }
        public float Opacity { get; set; }
        public Color TransparentColor { get; set; }
        public RotateFlipType RotateFlip { get; set; }
        public Padding Margin { get; set; }
        public float ScaleRatio { get; set; }
        public Font TextFont { get; set; }
        public Color TextFontColor { get; set; }
        public void ResetImage()
        {
            if (this.Image != null)
            {
                this.Image.Dispose();
                this.Image = null;
            }

            this.Image = new Bitmap(_originalImage);
        }

        public void DrawImage(string filename)
        {
            DrawImage(Image.FromFile(filename));
        }

        public void DrawImage(Image watermark)
        {
            if (watermark == null)
                throw new ArgumentNullException("watermark");

            if (Opacity < 0 || Opacity > 1)
                throw new ArgumentOutOfRangeException("Opacity");

            if (ScaleRatio <= 0)
                throw new ArgumentOutOfRangeException("ScaleRatio");

            // Creates a new watermark with margins (if margins are not specified returns the original watermark)
            _watermark = GetWatermarkImage(watermark);

            // Rotates and/or flips the watermark
            _watermark.RotateFlip(RotateFlip);

            // Calculate watermark position
            Point watermarkPosition = GetWatermarkPosition();

            // Watermark destination rectangle
            Rectangle destRect = new Rectangle(watermarkPosition.X, watermarkPosition.Y, _watermark.Width, _watermark.Height);

            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][] {
                    new float[] { 1, 0f, 0f, 0f, 0f},
                    new float[] { 0f, 1, 0f, 0f, 0f},
                    new float[] { 0f, 0f, 1, 0f, 0f},
                    new float[] { 0f, 0f, 0f, Opacity, 0f},
                    new float[] { 0f, 0f, 0f, 0f, 1}
                      });

            var attributes = new ImageAttributes();

            // Set the opacity of the watermark
            attributes.SetColorMatrix(colorMatrix);

            // Set the transparent color 
            if (TransparentColor != Color.Empty)
            {
                attributes.SetColorKey(TransparentColor, TransparentColor);
            }

            // Draw the watermark
            using (var gr = Graphics.FromImage(this.Image))
            {
                gr.DrawImage(_watermark, destRect, 0, 0, _watermark.Width, _watermark.Height, GraphicsUnit.Pixel, attributes);
            }
        }

        public void DrawText(string text)
        {
            // Convert text to image, so we can use opacity etc.
            Image textWatermark = GetTextWatermark(text);

            DrawImage(textWatermark);
        }

        private void LoadImage(Image image)
        {
            _originalImage = image;
            ResetImage();
        }

        private Image GetWatermarkImage(Image watermark)
        {
            // If there are no margins specified and scale ration is 1, no need to create a new bitmap
            if (Margin.All == 0 && ScaleRatio == 1.0f)
                return watermark;

            // Create a new bitmap with new sizes (size + margins) and draw the watermark
            int newWidth = Convert.ToInt32(watermark.Width * ScaleRatio);
            int newHeight = Convert.ToInt32(watermark.Height * ScaleRatio);

            Rectangle destRect = new Rectangle(Margin.Left, Margin.Top, newWidth, newHeight);
            Rectangle sourceRect = new Rectangle(0, 0, watermark.Width, watermark.Height);

            Bitmap bitmap = new Bitmap(newWidth + Margin.Left + Margin.Right, newHeight + Margin.Top + Margin.Bottom);
            bitmap.SetResolution(watermark.HorizontalResolution, watermark.VerticalResolution);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(watermark, destRect, sourceRect, GraphicsUnit.Pixel);
            }

            return bitmap;
        }

        private Image GetTextWatermark(string text)
        {
            SizeF size;

            // Figure out the size of the box to hold the watermarked text
            using (Graphics g = Graphics.FromImage(this.Image))
            {
                size = g.MeasureString(text, TextFont);
            }

            // Create a new bitmap for the text, and, actually, draw the text
            Bitmap textBitmap = new Bitmap((int)size.Width, (int)size.Height);
            textBitmap.SetResolution(this.Image.HorizontalResolution, this.Image.VerticalResolution);

            Brush brush = new SolidBrush(TextFontColor);
            using (Graphics g = Graphics.FromImage(textBitmap))
            {
                g.DrawString(text, TextFont, brush, 0, 0);
            }

            return textBitmap;
        }

        private Point GetWatermarkPosition()
        {
            int x = 0;
            int y = 0;

            switch (Position)
            {
                case WatermarkPosition.Absolute:
                    x = AbsolutePositionX; y = AbsolutePositionY;
                    break;
                case WatermarkPosition.TopLeft:
                    x = 0; y = 0;
                    break;
                case WatermarkPosition.TopRight:
                    x = this.Image.Width - _watermark.Width; y = 0;
                    break;
                case WatermarkPosition.TopMiddle:
                    x = (this.Image.Width - _watermark.Width) / 2; y = 0;
                    break;
                case WatermarkPosition.BottomLeft:
                    x = 0; y = this.Image.Height - _watermark.Height;
                    break;
                case WatermarkPosition.BottomRight:
                    x = this.Image.Width - _watermark.Width; y = this.Image.Height - _watermark.Height;
                    break;
                case WatermarkPosition.BottomMiddle:
                    x = (this.Image.Width - _watermark.Width) / 2; y = this.Image.Height - _watermark.Height;
                    break;
                case WatermarkPosition.MiddleLeft:
                    x = 0; y = (this.Image.Height - _watermark.Height) / 2;
                    break;
                case WatermarkPosition.MiddleRight:
                    x = this.Image.Width - _watermark.Width; y = (this.Image.Height - _watermark.Height) / 2;
                    break;
                case WatermarkPosition.Center:
                    x = (this.Image.Width - _watermark.Width) / 2; y = (this.Image.Height - _watermark.Height) / 2;
                    break;
                default:
                    break;
            }

            return new Point(x, y);
        }
    }
}
