using MobileKoisk.ViewModel;

namespace MobileKoisk.View;

public partial class MyBasketPage : ContentPage
{
	public MyBasketPage()
	{
		InitializeComponent();
        BindingContext = new BasketPageViewModel();
	}

	private async void GoToPayment(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("paymentpage", true);
    }
}