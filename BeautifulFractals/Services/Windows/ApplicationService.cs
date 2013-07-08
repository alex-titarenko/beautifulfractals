using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TAlex.BeautifulFractals.Services.Windows
{
    public class ApplicationService : IApplicationService
    {
        #region IApplicationService Members

        public void Shutdown()
        {
            Application.Current.Shutdown();
        }

        #endregion
    }
}
