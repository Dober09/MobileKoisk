using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using MobileKoisk.Helper;
using MobileKoisk.Models;

namespace MobileKoisk.View;

public partial class ScannedPopup : Popup
{

	private readonly ProductItem _productItem;
	public ScannedPopup(ProductItem productItem)
	{
		
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