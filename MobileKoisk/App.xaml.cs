using MobileKoisk.View;

namespace MobileKoisk
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; set; }
        public App(IServiceProvider service)
        {
            Services = service;
            //Services
            InitializeComponent();
            MainPage = new AppShell();

        }
    }
}
