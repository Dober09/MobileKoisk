namespace MobileKoisk.View;
using MobileKoisk.Models;
using MobileKoisk.ViewModel;

public partial class ProductListPage : ContentPage
{
	
	public ProductListPage( string category)
	{
		InitializeComponent();
		var viewModel = new ProductListViewModel(category);
        BindingContext = viewModel;
	}


}