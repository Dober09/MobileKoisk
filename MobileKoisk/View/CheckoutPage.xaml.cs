using MobileKoisk.ViewModel;
namespace MobileKoisk.View;


public partial class CheckoutPage : ContentPage
{
	public CheckoutPage()
	{
		InitializeComponent();
	}

    private async void BackClicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }
}