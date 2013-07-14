namespace TAlex.BeautifulFractals.Services.Windows
{
    public interface IFontChooserDialogService
    {
        string SelectedFontFamily { get; set; }
        double SelectedFontSize { get; set; }
        Rendering.Color SelectedFontColor { get; set; }

        bool? ShowDialog();
    }
}
