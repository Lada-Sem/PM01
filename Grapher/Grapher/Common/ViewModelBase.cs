using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Grapher.Common
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        protected void Set<T>(T value, ref T field, [CallerMemberName] string prop = "")
        {
            if (field != null && field.Equals(value))
                return;
            field = value;
            OnPropertyChanged(prop);
        }
    }
}
