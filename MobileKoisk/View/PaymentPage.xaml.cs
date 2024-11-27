using MobileKoisk.ViewModel;
namespace MobileKoisk.View;

public partial class PaymentPage : ContentPage
{
	public PaymentPage()
	{
		InitializeComponent();
		BindingContext = new PaymentViewModel();

	}
}