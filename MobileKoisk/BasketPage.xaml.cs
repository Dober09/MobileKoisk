using MobileKoisk.ViewModel;

namespace MobileKoisk;

public partial class BasketPage : ContentPage
{
	public BasketPage()
	{
		InitializeComponent();
		BindingContext = new BasketPageViewModel();
	}
}