using System;
using System.ComponentModel;
using System.Xml.Serialization;


namespace TAlex.BeautifulFractals.Fractals
{
    public abstract class Fractal : INotifyPropertyChanged
    {
        #region Fields

        private bool _display;

        #endregion

        #region Properties

        public abstract string Caption { get; }

        public bool Display
        {
            get
            {
                return _display;
            }

            set
            {
                _display = value;
                OnPropertyChanged("Display");
            }
        }

        #endregion

        #region Constructors

        public Fractal()
        {
            Display = true;
        }
        
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
