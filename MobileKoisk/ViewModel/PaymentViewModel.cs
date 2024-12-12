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
	public class PaymentViewModel : BaseViewModel
	{


        private bool _isCreditCardSelected;

        private bool _isCreditCardButtonSelected;

        public bool IsCreditCardSelected
        {
            get => _isCreditCardSelected;
            set
            {
                // Update the property value and notify listeners if it changes
                if (_isCreditCardSelected != value)
                {
                    _isCreditCardSelected = value;
                    OnPropertyChanged(nameof(IsCreditCardSelected));
                }
            }
        }
        // Public property to access and modify the credit card button selection state
        public bool IsCreditCardButtonSelected
		{
			get => _isCreditCardButtonSelected;
			set
			{
				// Update the property value and notify listeners if it changes
				if (_isCreditCardButtonSelected != value)
				{
					_isCreditCardButtonSelected = value;
					OnPropertyChanged(nameof(IsCreditCardButtonSelected));
				}
			}
		}

		// Command to toggle the selection state of the credit card button and visibility
		public ICommand SelectCreditCardCommand => new Command(() =>
		{
			// Toggle the button's selected state
			IsCreditCardButtonSelected = !IsCreditCardButtonSelected;
			// Toggle the visibility of the credit card option
			IsCreditCardSelected = !IsCreditCardSelected;
		});

		//// Event for notifying property changes to the UI or other listeners
		//public event PropertyChangedEventHandler PropertyChanged;

		//// Helper method to raise the PropertyChanged event for a specific property
		//protected void OnPropertyChanged(string propertyName)
		//{
		//	PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		//}
	}
}
