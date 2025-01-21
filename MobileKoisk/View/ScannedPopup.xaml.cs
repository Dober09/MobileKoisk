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

    private void AddToMonkey(object sender, EventArgs e)
    {
		WeakReferenceMessenger.Default.Send(new AddToBasketMessage(_productItem));
		Close();
    }
}