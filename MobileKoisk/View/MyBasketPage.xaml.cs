using MobileKoisk.ViewModel;

namespace MobileKoisk.View;

public partial class MyBasketPage : ContentPage
{
	public MyBasketPage()
	{
		InitializeComponent();
        BindingContext = new BasketPageViewModel();
    }
}