namespace MobileKoisk.View;
using MobileKoisk.Models;
using MobileKoisk.Services;
using MobileKoisk.ViewModel;
using Microsoft.Extensions.DependencyInjection;

public partial class ProductListPage : ContentPage
{
	
	public ProductListPage( string category)
	{
		InitializeComponent();
		
		var app = (App)App.Current;
		// Access the service provider
		var wishListServices = app.Services.GetService<WishListServices>();

		if (wishListServices == null)
		{
			throw new Exception("WishListServices is not registered in the service provider.");
		}
		// Pass the resolved WishListServices and category to the ViewModel
		var viewModel = new ProductListViewModel(wishListServices, category);
        BindingContext = viewModel;
	}


}