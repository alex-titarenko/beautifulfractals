using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TAlex.BeautifulFractals.Fractals;
using TAlex.BeautifulFractals.Infrastructure;
using TAlex.BeautifulFractals.Rendering;

namespace TAlex.BeautifulFractals.Views
{
    /// <summary>
    /// Interaction logic for PreviewWindow.xaml
    /// </summary>
    public partial class PreviewWindow : Window
    {
        public ICollectionView FractalCollection { get; set; }

        
        public PreviewWindow()
        {
            InitializeComponent();          
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RenderFractal();
        }

        private void border_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //RenderFractal();
        }

        public void RenderFractal()
        {
            Task.Run(() =>
            {
                WriteableBitmap wb = BitmapFactory.New((int)border.ActualWidth, (int)border.ActualHeight);
                Fractal targetFractal = FractalCollection.CurrentItem as Fractal;

                if (targetFractal is Fractal2D)
                {
                    using (wb.GetBitmapContext())
                    {               
                        WriteableBitmapGraphicsContext context = new WriteableBitmapGraphicsContext(wb);
                        ((Fractal2D)targetFractal).Render(context);
                    }
                }

                wb.Freeze();
                Dispatcher.Invoke(() => { previewImage.Source = wb; });
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FractalCollection.MoveCurrentToPrevious();
            RenderFractal();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FractalCollection.MoveCurrentToNext();
            RenderFractal();
        }
    }
}
