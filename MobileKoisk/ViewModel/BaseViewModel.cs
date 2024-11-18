using System.ComponentModel;

namespace MobileKoisk.ViewModel
{
    class BaseViewModel : INotifyPropertyChanged
    {
       
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
