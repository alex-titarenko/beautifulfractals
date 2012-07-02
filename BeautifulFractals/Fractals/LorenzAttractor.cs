using System;
using System.Collections.Generic;
using System.Text;

using TAlex.BeautifulFractals.Rendering;
using TAlex.BeautifulFractals.Rendering.ColorPalettes;


namespace TAlex.BeautifulFractals.Fractals
{
    /// <summary>
    /// 
    /// </summary>
    public class LorenzAttractor : Fractal2D
    {
        #region Properties

        public override string Caption
        {
            get
            {
                return String.Format("Lorenz attractor (σ = {0}, ρ = {1}, β = {2})", Sigma, Rho, Beta);
            }
        }

        public int Iterations
        {
            get;
            set;
        }

        public double Sigma
        {
            get;
            set;
        }

        public double Rho
        {
            get;
            set;
        }

        public double Beta
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public LorenzAttractor()
        {
            Iterations = 1000000;

            Sigma = 5;
            Rho = 15;
            Beta = 1;
        }

        public LorenzAttractor(Color color)
            : this()
        {
            Color = color;
        }

        public LorenzAttractor(double sigma, double rho, double beta, Color color)
            : this(color)
        {
            Sigma = sigma;
            Rho = rho;
            Beta = beta;
        }

        #endregion

        #region Methods

        public override void Render(IGraphics2DContext context)
        {
            double mx = context.Viewport.Width / 2;
            double my = context.Viewport.Height / 2;

            double x = 3.051522;
            double y = 1.582542;
            double z = 15.62388;

            double xn, yn, zn;

            double dt = 0.0001;

            for (int i = 0; i < Iterations; i++)
            {
                xn = x + Sigma * (y - x) * dt;
                yn = y + (x * (Rho - z) - y) * dt;
                zn = z + (x * y - Beta * z) * dt;
                x = xn; y = yn; z = zn;

                context.PutPixel(19.3 * (y - x * 0.292893) + mx, -11 * (z + x * 0.292893) + my, Color);
            }
        }

        public override string ToString()
        {
            return String.Format("Lorenz Attractor");
        }

        #endregion
    }
}
