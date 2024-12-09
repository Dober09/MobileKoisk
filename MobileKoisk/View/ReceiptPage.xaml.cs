using Microsoft.Maui.Controls;
using MobileKoisk.ViewModel;
namespace MobileKoisk.View;

public partial class ReceiptPage : ContentPage
{
	public ReceiptPage()
	{
		InitializeComponent();
		BindingContext = new ReceiptPageViewModel();
	}
}