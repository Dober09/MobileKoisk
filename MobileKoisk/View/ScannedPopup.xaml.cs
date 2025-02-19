using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using MobileKoisk.Helper;
using MobileKoisk.Models;
using MobileKoisk.Services;

namespace MobileKoisk.View;

public partial class ScannedPopup : Popup
{

	private readonly Product _productItem;
    private readonly BadgeCounterService _badgeCounterService;
    public ScannedPopup(Product productItem, BadgeCounterService badgeCounterService)
	{
		
		_badgeCounterService = badgeCounterService;
		_productItem = productItem;
		InitializeComponent();
		ProductImage.Source = productItem.image_url;
		ProductName.Text = productItem.item_description;
		Price.Text = $"R {productItem.selling_price}";
		//ItemLeft.Text = $"{productItem.quantity} left";
    }

    private  void AddToBasket(object sender, EventArgs e)
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"AddToBasket clicked for product: {_productItem.item_description}");

            BasketService.AddItem( _productItem );
       
            BadgeCounterService.SetCount(BadgeCounterService.Count + 1);
            Close();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in AddToBasket: {ex.Message}");
        }
    }
}