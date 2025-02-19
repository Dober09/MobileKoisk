using MobileKoisk.ViewModel;
using MobileKoisk.Services;


using MobileKoisk.Helper;



namespace MobileKoisk.View;

public partial class MyBasketPage : ContentPage , IDisposable
{
    private BasketPageViewModel _viewModel;
    public MyBasketPage()
	{
		InitializeComponent();
        _viewModel = new BasketPageViewModel();
        BindingContext = _viewModel;
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

    

    public void Dispose() {

      
        BadgeCounterService.CountChanged -= OnCountChanged;
        System.Diagnostics.Debug.WriteLine("MyBasketPage disposed");
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        BasketService.ClearBasket();
        BadgeCounterService.SetCount(0);
    }
}