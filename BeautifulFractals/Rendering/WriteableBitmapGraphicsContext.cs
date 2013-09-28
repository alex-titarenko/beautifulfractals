using System;
using System.Windows.Media.Imaging;


namespace TAlex.BeautifulFractals.Rendering
{
    public class WriteableBitmapGraphicsContext : IGraphics2DContext
    {
        #region Fields

        private WriteableBitmap _context;

        #endregion

        #region Constructor

        public WriteableBitmapGraphicsContext(WriteableBitmap context)
        {
            _context = context;
        }

        #endregion


        #region IGraphics2DContext Members

        public Rectangle Viewport
        {
            get { return new Rectangle(0, 0, _context.PixelWidth, _context.PixelHeight); }
        }

        public void Clear(Color color)
        {
            _context.Clear(ColorToWpfColor(color));
        }

        public void Clear(LinearGradientBrush brush)
        {
            throw new NotImplementedException();
        }

        public void Invalidate()
        {
            if (_context.CanFreeze)
            {
                _context.Freeze();
            }
        }

        public void PutPixel(int x, int y, Color color)
        {
            if (x >= 0 && x < _context.PixelWidth && y >= 0 && y < _context.PixelHeight)
            {
                _context.SetPixel(x, y, color.ToArgb());
            }
        }

        public void PutPixel(double x, double y, Color color)
        {
            PutPixel((int)Math.Round(x), (int)Math.Round(y), color);
        }

        public void PutPixel(Point p, Color color)
        {
            PutPixel(p.X, p.Y, color);
        }

        public void DrawLine(Point p1, Point p2, Color color)
        {
            DrawLine(p1.X, p1.Y, p2.X, p2.Y, color);
        }

        public void DrawLine(double x1, double y1, double x2, double y2, Color color)
        {
            _context.DrawLineAa((int)Math.Round(x1), (int)Math.Round(y1), (int)Math.Round(x2), (int)Math.Round(y2), color.ToArgb());
        }

        public void FillRectangle(double x, double y, double width, double height, Color color)
        {
            _context.FillRectangle((int)Math.Round(x), (int)Math.Round(y), (int)Math.Round(x + width), (int)Math.Round(y + height), color.ToArgb());
        }

        public void FillRectangle(double x, double y, double width, double height, LinearGradientBrush brush)
        {
            throw new NotImplementedException();
        }

        public void DrawRectangle(double x, double y, double width, double height, Color color)
        {
            _context.DrawRectangle((int)Math.Round(x), (int)Math.Round(y), (int)Math.Round(x + width), (int)Math.Round(y + height), color.ToArgb());
        }

        public void DrawEllipse(double x, double y, double width, double height, Color color)
        {
            _context.DrawEllipse((int)Math.Round(x), (int)Math.Round(y), (int)Math.Round(x + width), (int)Math.Round(y + height), color.ToArgb());
        }

        public void DrawString(string text, Font font, Color foreground, Point location)
        {
            throw new NotImplementedException();
        }

        public Size MeasureString(string text, Font font)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Invalidate();
        }

        #endregion

        #region Helpers

        private System.Windows.Media.Color ColorToWpfColor(Color color)
        {
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        #endregion
    }
}
