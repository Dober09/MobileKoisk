using MobileKoisk.ViewModel;
namespace MobileKoisk.View;

public partial class MorePage : ContentPage
{
	public MorePage()
	{
		InitializeComponent();
		BindingContext = new MoreVeiwModel();
	} 
}