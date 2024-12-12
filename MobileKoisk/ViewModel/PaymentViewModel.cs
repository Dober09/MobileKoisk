using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace MobileKoisk.ViewModel
{
	// ViewModel for managing payment options in a mobile kiosk application
	public class PaymentViewModel : INotifyPropertyChanged
	{



		public event PropertyChangedEventHandler? PropertyChanged ;
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		} 

	}
}
