using MobileKoisk.ViewModel;

namespace MobileKoisk.View;

public partial class WishListPage : ContentPage
{
	public WishListPage()
	{
		InitializeComponent();
		BindingContext = new ProductListViewModel("WishList");
	}
}