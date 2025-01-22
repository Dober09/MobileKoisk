using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using MobileKoisk.Helper;
using MobileKoisk.Models;

namespace MobileKoisk.View;

public partial class ScannedPopup : Popup
{

	private readonly Product _productItem;
	public ScannedPopup(Product productItem)
	{
		
		_productItem = productItem;
		InitializeComponent();

		ProductName.Text = productItem.item_description;
		Price.Text = $"R {productItem.selling_price}";
		ItemLeft.Text = $"{productItem.quantity} left";
    }

    private  void AddToMonkey(object sender, EventArgs e)
    {
		try {
			System.Diagnostics.Debug.WriteLine($"Sending product: {_productItem.item_description}");
			WeakReferenceMessenger.Default.Send(new AddToBasketMessage(_productItem));
			System.Diagnostics.Debug.WriteLine("Message sent");
			Close();	
		}catch(Exception ex) {
			System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
		}
    }
}