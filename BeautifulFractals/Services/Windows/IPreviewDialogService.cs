using System;
using System.Collections;
using System.Collections.Generic;
using TAlex.BeautifulFractals.Fractals;
using TAlex.BeautifulFractals.Infrastructure;


namespace TAlex.BeautifulFractals.Services.Windows
{
    public interface IPreviewDialogService
    {
        void Show(ICollectionView fractalCollection);
    }
}
