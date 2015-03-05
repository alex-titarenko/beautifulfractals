using System;
using System.Collections.Generic;
using System.Text;

namespace TAlex.BeautifulFractals.Rendering
{
    public interface IGraphics2DContext : IDisposable
    {
        Rectangle Viewport { get; }

        void Clear(Color color);

        void Clear(LinearGradientBrush brush);

        void Invalidate();

        void PutPixel(int x, int y, Color color);

        void PutPixel(double x, double y, Color color);

        void PutPixel(Point p, Color color);

        void DrawLine(Point p1, Point p2, Color color);

        void DrawLine(double x1, double y1, double x2, double y2, Color color);

        void FillRectangle(double x, double y, double width, double height, Color color);

        void FillRectangle(double x, double y, double width, double height, LinearGradientBrush brush);

        void DrawRectangle(double x, double y, double width, double height, Color color);

        void DrawEllipse(double x, double y, double width, double height, Color color);

        void DrawString(string text, Font font, Color foreground, Point location);

        Size MeasureString(string text, Font font);
    }
}
