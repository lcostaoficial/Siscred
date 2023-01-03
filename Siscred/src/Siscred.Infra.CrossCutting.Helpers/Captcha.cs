using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Siscred.Infra.CrossCutting.Helpers
{
    public class Captcha
    {
        public string Text { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string FamilyName { get; set; }
        private Bitmap image;
        private static Random random = new Random();

        public Bitmap Image
        {
            get
            {
                if (!string.IsNullOrEmpty(Text) && Height > 0 && Width > 0)
                    GenerateImage();
                return image;
            }
        }

        public Captcha() { }

        ~Captcha()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                image.Dispose();
        }

        private void SetDimensions(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException("width", width, "Argument out of range, must be greater than zero.");
            if (height <= 0)
                throw new ArgumentOutOfRangeException("height", height, "Argument out of range, must be greater than zero.");
            Width = width;
            Height = height;
        }

        private void SetFamilyName(string familyName)
        {
            try
            {
                Font font = new Font(FamilyName, 16F);
                FamilyName = familyName;
                font.Dispose();
            }
            catch
            {
                FamilyName = FontFamily.GenericSerif.Name;
            }
        }

        public void GenerateImage()
        {
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, Width, Height);

            HatchBrush hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.FromArgb(114, 172, 236), Color.FromArgb(161, 214, 255));
            g.FillRectangle(hatchBrush, rect);

            SizeF size;
            float fontSize = Height + 4;
            Font font;
            do
            {
                fontSize--;
                font = new Font(FamilyName, fontSize, FontStyle.Bold);
                size = g.MeasureString(Text, font);
            } while (size.Width > Width);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            GraphicsPath path = new GraphicsPath();
            path.AddString(Text, font.FontFamily, (int)font.Style, font.Size, rect, format);
            float v = 4F;
            PointF[] points =
                {
                    new PointF(random.Next(Width) / v, random.Next(Height) / v),
                    new PointF(Width - random.Next(Width) / v, random.Next(Height) / v),
                    new PointF(random.Next(Width) / v, Height - random.Next(Height) / v),
                    new PointF(Width - random.Next(Width) / v, Height - random.Next(Height) / v)
                };
            Matrix matrix = new Matrix();
            matrix.Translate(0F, 0F);
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

            hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, ColorTranslator.FromHtml("#000000"), ColorTranslator.FromHtml("#000000"));
            g.FillPath(hatchBrush, path);

            int m = Math.Max(Width, Height);
            for (int i = 0; i < (int)(Width * Height / 30F); i++)
            {
                int x = random.Next(Width);
                int y = random.Next(Height);
                int w = random.Next(m / 50);
                int h = random.Next(m / 50);
                g.FillEllipse(hatchBrush, x, y, w, h);
            }

            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();

            image = bitmap;
        }

        public static string GenerateRandomCode()
        {
            string s = "";
            for (int i = 0; i < 6; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}