using Grapher.Common;
using Grapher.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Grapher.ViewModels.Views
{
    public class InfoViewModel : ViewModelBase
    {
        private string _manifest;

        public string Manifest
        {
            get => _manifest;
            set => Set(value, ref _manifest);
        }


        public InfoViewModel()
        {
            Manifest = Resources.Manifest;
        }
    }
}
