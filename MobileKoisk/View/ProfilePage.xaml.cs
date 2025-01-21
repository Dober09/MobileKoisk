namespace MobileKoisk.View;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

    private async void GoToPrevPage(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }
}