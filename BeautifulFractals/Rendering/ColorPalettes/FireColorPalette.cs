using System;
using System.Collections.Generic;
using System.Text;

namespace TAlex.BeautifulFractals.Rendering.ColorPalettes
{
    public class FireColorPalette : PredefinedColorPalette
    {
        static FireColorPalette()
        {
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(0, 0, 0), 0.0));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(255, 0, 0), 0.25));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(255, 255, 0), 0.5));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(255, 255, 255), 0.75));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(0, 0, 0), 1.0));
        }

        public FireColorPalette()
        {
        }
    }
}
