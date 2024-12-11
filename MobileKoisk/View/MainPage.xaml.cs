using MobileKoisk.ViewModel;
namespace MobileKoisk.View;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new BasketPageViewModel();
		
	}
}