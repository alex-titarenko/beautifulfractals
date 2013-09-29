using System;


namespace TAlex.BeautifulFractals.Fractals
{
    public abstract class GeometricFractal2D : Fractal2D
    {
        public int Iterations { get; set; }

        public override bool FullyFillRendering
        {
            get
            {
                return false;
            }
        }
    }
}
