using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace RevitCommon.WPF
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public void NotifyPropertyChanged(string propname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
        public void NotifyPropertiesChanged(params string[] paranames)
        {
            foreach (var para in paranames)
            {
                NotifyPropertyChanged(para);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
