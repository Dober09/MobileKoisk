using MobileKoisk.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MobileKoisk.ViewModel
{
    public class MoreVeiwModel
    {
      

        public MoreVeiwModel()
        {
           
               

            GoToPayment = new Command(async () => { await Shell.Current.GoToAsync("checkoutpage"); });

        }

        public ICommand GoToPayment { get; }
    }
}
