using MobileKoisk.ViewModel;

namespace MobileKoisk.View;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfileViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private async void GoToPrevPage(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }
}