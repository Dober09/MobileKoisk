namespace MobileKoisk
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CheckoutPage), typeof(CheckoutPage));
			Routing.RegisterRoute(nameof(BasketPage), typeof(BasketPage));
		}
    }
}
