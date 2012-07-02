using System;
using System.Collections.Generic;
using System.Text;

namespace TAlex.BeautifulFractals.Rendering.ColorPalettes
{
    public class RainbowColorPalette : PredefinedColorPalette
    {
        static RainbowColorPalette()
        {
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(255, 0, 255), 0.0));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(0, 0, 0), 0.142));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(255, 0, 0), 0.285));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(255, 255, 0), 0.428));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(0, 255, 0), 0.571));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(0, 255, 255), 0.714));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(0, 0, 255), 0.857));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(255, 0, 255), 1.0));
        }

        public RainbowColorPalette()
        {
        }
    }
}
