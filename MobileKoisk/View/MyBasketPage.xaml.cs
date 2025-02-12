using MobileKoisk.ViewModel;
using MobileKoisk.Services;

namespace MobileKoisk.View;

public partial class MyBasketPage : ContentPage , IDisposable
{
	public MyBasketPage()
	{
		InitializeComponent();
        BindingContext = new BasketPageViewModel();
        BadgeCounterService.CountChanged += OnCountChanged;
	}


    private void OnCountChanged(object? sender,int newCount)
    {
       // CountValue.Text = $"Current Value :{newCount} ";
    }
	private async void GoToPayment(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("PaymentPage", true);
    }

    //private void UpBagdeCounter(object sender, EventArgs e)
    //{
    //    BadgeCounterService.SetCount(BadgeCounterService.Count + 1);
    //}

    //private void DownBageCounter(object sender, EventArgs e)
    //{
    //    BadgeCounterService.SetCount(BadgeCounterService.Count - 1);

    //}
    public void Dispose () => BadgeCounterService.CountChanged -= OnCountChanged;

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        BadgeCounterService.SetCount(0);
    }
}