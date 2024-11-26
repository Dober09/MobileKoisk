using MobileKoisk.ViewModel;
namespace MobileKoisk.View;


public partial class CheckoutPage : ContentPage
{
	public CheckoutPage()
	{
		InitializeComponent();
		BindingContext = new PaymentViewModel();

	}
}