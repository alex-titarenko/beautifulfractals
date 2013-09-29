using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Interop;
using GdiPlus = System.Drawing;

using TAlex.BeautifulFractals.Rendering;
using TAlex.BeautifulFractals.Helpers;
using System.Windows;


namespace TAlex.BeautifulFractals.Rendering
{
    /// <summary>
    /// Represents the GDI and GDI+ graphics context.
    /// </summary>
    public class GdiGraphicsContext : IGraphics2DContext
    {
        #region Fields

        private System.Windows.Window _window;
        private IntPtr _hwnd;
        private IntPtr _hdc;
        private GdiPlus.Graphics _graphics;
        private GdiPlus.Pen _currentPen;
        private GdiPlus.SolidBrush _currentBrush;

        private bool _dispose = false;

        #endregion

        #region Properties

        public double ViewportPadding { get; set; }

        #endregion

        #region Constructors

        public GdiGraphicsContext(System.Windows.Window window)
        {
            _window = window;
            
            WindowInteropHelper helper = new WindowInteropHelper(window);
            _hwnd = helper.Handle;
            _hdc = Win32.GetDC(_hwnd);

            _graphics = GdiPlus.Graphics.FromHdc(_hdc);
            _graphics.SmoothingMode = GdiPlus.Drawing2D.SmoothingMode.AntiAlias;

            _currentPen = new GdiPlus.Pen(GdiPlus.Color.Black);
            _currentBrush = new GdiPlus.SolidBrush(GdiPlus.Color.Black);
        }

        #endregion

        #region Methods

        #region IGraphics2DContext Members

        public Rectangle Viewport
        {
            get
            {
                Size scale = GetScalingFactor();
                double w = _window.ActualWidth * scale.Width;
                double h = _window.ActualHeight * scale.Height;

                if (ViewportPadding == 0.0)
                    return new Rectangle(0, 0, w, h);
                else
                    return new Rectangle(ViewportPadding, ViewportPadding,
                        w - 2 * ViewportPadding, h - 2 * ViewportPadding);
            }
        }

        public void Clear(Color color)
        {
            _graphics.Clear(ColorToGdiColor(color));
        }

        public void Clear(LinearGradientBrush brush)
        {
            Size scale = GetScalingFactor();
            FillRectangle(-1, -1, _window.ActualWidth * scale.Width + 1, _window.ActualHeight * scale.Height + 1, brush);
        }

        public void Invalidate()
        {
            Win32.InvalidateRect(_hwnd, IntPtr.Zero, true);
        }

        public void PutPixel(int x, int y, Color color)
        {
            Win32.SetPixel(_hdc, x, y, (uint)ColorToRGB(color));
        }

        public void PutPixel(double x, double y, Color color)
        {
            PutPixel((int)x, (int)y, color);
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
            _currentPen.Color = ColorToGdiColor(color);
            _graphics.DrawLine(_currentPen, (float)x1, (float)y1, (float)x2, (float)y2);
        }

        public void FillRectangle(double x, double y, double width, double height, Color color)
        {
            _currentBrush.Color = ColorToGdiColor(color);
            _graphics.FillRectangle(_currentBrush, (float)x, (float)y, (float)width, (float)height);
        }

        public void FillRectangle(double x, double y, double width, double height, LinearGradientBrush brush)
        {
            GdiPlus.RectangleF rect = new GdiPlus.RectangleF((float)x, (float)y, (float)width, (float)height);

            GdiPlus.Drawing2D.LinearGradientBrush gdiBrush =
                new GdiPlus.Drawing2D.LinearGradientBrush(rect, ColorToGdiColor(brush.FromColor),
                    ColorToGdiColor(brush.ToColor), (float)brush.Angle);
            _graphics.FillRectangle(gdiBrush, rect);
        }

        public void DrawRectangle(double x, double y, double width, double height, Color color)
        {
            _currentPen.Color = ColorToGdiColor(color);
            _graphics.DrawRectangle(_currentPen, (float)x, (float)y, (float)width, (float)height);
        }

        public void DrawEllipse(double x, double y, double width, double height, Color color)
        {
            _currentPen.Color = ColorToGdiColor(color);
            _graphics.DrawEllipse(_currentPen, (float)x, (float)y, (float)width, (float)height);
        }

        public void DrawString(string text, Font font, Color foreground, Point location)
        {
            _currentBrush.Color = ColorToGdiColor(foreground);
            _graphics.DrawString(text, ToGdiFont(font), _currentBrush, (float)location.X, (float)location.Y); 
        }

        public Size MeasureString(string text, Font font)
        {
            GdiPlus.SizeF size = _graphics.MeasureString(text, ToGdiFont(font));
            return new Size(size.Width, size.Height);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (!_dispose)
            {
                Win32.ReleaseDC(_hwnd, _hdc);
                _graphics.Dispose();
                _dispose = true;
            }
        }

        #endregion

        #region Helpers

        private int ColorToRGB(Color crColor)
        {
            return crColor.B << 16 | crColor.G << 8 | crColor.R;
        }

        private GdiPlus.Color ColorToGdiColor(Color color)
        {
            return GdiPlus.Color.FromArgb(color.R, color.G, color.B);
        }

        private GdiPlus.Font ToGdiFont(Font font)
        {
            return new GdiPlus.Font(font.FamilyName, (float)font.Size, GdiPlus.GraphicsUnit.Pixel);
        }

        private Size GetScalingFactor()
        {
            double scaleX = 1, scaleY = 1;
            _window.Dispatcher.Invoke(() =>
            {
                PresentationSource source = PresentationSource.FromVisual(_window);
                if (source != null)
                {
                    scaleX = source.CompositionTarget.TransformToDevice.M11;
                    scaleY = source.CompositionTarget.TransformToDevice.M22;
                }
            });
            return new Size { Width = scaleX, Height = scaleY };
        }

        #endregion

        #endregion
    }
}
