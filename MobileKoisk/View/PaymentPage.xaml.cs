using MobileKoisk.ViewModel;
namespace MobileKoisk.View;

public partial class PaymentPage : ContentPage
{
	public PaymentPage()
	{
		InitializeComponent();
		BindingContext = new BasketPageViewModel();
	}

	private async void GoToReceiptPage(object sender, EventArgs e)
	{

		await Shell.Current.GoToAsync("ReciptPage");
	}

    private async void GoToPrevPage(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }
}