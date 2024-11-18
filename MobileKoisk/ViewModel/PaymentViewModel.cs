using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace MobileKoisk.ViewModel
{
	public class PaymentViewModel : INotifyPropertyChanged
	{
		private bool _isCreditCardSelected;
		private bool _isCreditCardButtonSelected;

		public bool IsCreditCardSelected
		{
			get => _isCreditCardSelected;
			set
			{
				if (_isCreditCardSelected != value)
				{
					_isCreditCardSelected = value;
					OnPropertyChanged(nameof(IsCreditCardSelected));
				}
			}
		}

		public bool IsCreditCardButtonSelected
		{
			get => _isCreditCardButtonSelected;
			set
			{
				if (_isCreditCardButtonSelected != value)
				{
					_isCreditCardButtonSelected = value;
					OnPropertyChanged(nameof(IsCreditCardButtonSelected));
				}
			}
		}

		public ICommand SelectCreditCardCommand => new Command(() =>
		{
			// Toggle button selection
			IsCreditCardButtonSelected = !IsCreditCardButtonSelected; 
			// Toggle visibility
			IsCreditCardSelected = !IsCreditCardSelected;
		});


		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
