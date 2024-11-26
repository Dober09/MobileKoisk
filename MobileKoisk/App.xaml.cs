using MobileKoisk.View;
namespace MobileKoisk
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new CheckoutPage();
        }
    }
}
