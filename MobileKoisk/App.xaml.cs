using MobileKoisk.View;

namespace MobileKoisk
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            Application.Current.MainPage = new AppShell();  // Set this in App.xaml.cs if not already done


        }
    }
}
