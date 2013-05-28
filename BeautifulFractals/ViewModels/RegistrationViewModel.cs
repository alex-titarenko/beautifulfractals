using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows.Input;


namespace TAlex.BeautifulFractals.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        public string LicenseName { get; set; }

        [Required]
        public string LicenseKey { get; set; }


        public ICommand RegisterCommand { get; set; }
    }
}
