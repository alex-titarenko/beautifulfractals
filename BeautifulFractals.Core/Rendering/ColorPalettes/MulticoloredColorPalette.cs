using System;
using System.Collections.Generic;
using System.Text;

namespace TAlex.BeautifulFractals.Rendering.ColorPalettes
{
    public class MulticoloredColorPalette : PredefinedColorPalette
    {
        static MulticoloredColorPalette()
        {
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(0, 0, 0), 0.0));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(0, 0, 255), 0.068333));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(0, 255, 255), 0.13667));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(255, 255, 255), 0.16667));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(255, 0, 0), 0.26667));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(0, 0, 0), 1 / 3.0));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(255, 255, 255), 0.5));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(0, 255, 0), 0.54167));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(0, 0, 0), 2 / 3.0));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(255, 80, 0), 0.74167));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(255, 255, 0), 0.80334));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(255, 255, 255), 0.83334));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(255, 0, 255), 0.91667));
            Palette.Transitions.Add(new TransitionColorPalette.Transition(Color.FromArgb(0, 0, 0), 1.0));
        }

        public MulticoloredColorPalette()
        {
        }
    }
}
