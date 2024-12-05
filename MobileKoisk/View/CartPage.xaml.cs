using MobileKoisk.ViewModel;

namespace MobileKoisk.View;

public partial class CartPage : ContentPage
{
	public CartPage(BasketPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}