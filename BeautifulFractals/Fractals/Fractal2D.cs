using TAlex.BeautifulFractals.Rendering;


namespace TAlex.BeautifulFractals.Fractals
{
    public abstract class Fractal2D : Fractal
    {
        public abstract void Render(IGraphics2DContext context);
    }
}
