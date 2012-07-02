using System;
using System.Collections.Generic;
using System.Text;

namespace TAlex.BeautifulFractals.Rendering.ColorPalettes
{
    public class OrangeBlueColorPalette : PredefinedColorPalette
    {
        static OrangeBlueColorPalette()
        {
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(0, 0, 0), 0.0));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(255, 80, 0), 0.2));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(255, 255, 0), 0.4));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(255, 255, 255), 0.6));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(0, 0, 255), 0.8));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(0, 0, 0), 1.0));
        }

        public OrangeBlueColorPalette()
        {
        }
    }
}
