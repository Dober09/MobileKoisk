using MobileKoisk.Services;
using MobileKoisk.ViewModel;
namespace MobileKoisk.View;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

		var dbService = new DatabaseService("localhost;Port=3360", "grocery_app", "aisha_seimela", "aisha@seimela2016");
		BindingContext = new ShoppingViewModel(dbService);
		
	}
}