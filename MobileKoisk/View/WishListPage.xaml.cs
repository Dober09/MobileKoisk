using MobileKoisk.ViewModel;

using MobileKoisk.Services;

namespace MobileKoisk.View;

public partial class WishListPage : ContentPage
{
	public WishListPage(WishListServices wishListServices)
	{
		InitializeComponent();
		BindingContext = wishListServices;
	}
}