using MobileKoisk.ViewModel;
namespace MobileKoisk.View;

public partial class MorePage : ContentPage
{
	public MorePage()
	{
		InitializeComponent();
		BindingContext = new MoreVeiwModel();
	}

    private async void onTappedButton(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("PaymentPage");
    }

    private async void GoToProfile(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("ProfilePage");
    }
}