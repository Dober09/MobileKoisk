namespace MobileKoisk.View;
using MobileKoisk.Models;
using MobileKoisk.ViewModel;
using System.Collections.ObjectModel;

public partial class ProductListPage : ContentPage
{
	
	public ProductListPage( string category)
	{
		InitializeComponent();
		var viewModel = new ProductListViewModel(category);
        BindingContext = viewModel;

	}
}