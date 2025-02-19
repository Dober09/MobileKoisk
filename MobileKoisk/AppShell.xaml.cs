
using MobileKoisk.View;

namespace MobileKoisk
{ 
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            RegisterRoutes();

		

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.AntiqueWhite);
                handler.PlatformView.Background = null;
#endif
            });

        }

        // Register routes
        private void RegisterRoutes()
        {
            Routing.RegisterRoute("IntroPage", typeof(IntroScreenPage));
            Routing.RegisterRoute("LoginRegisterPage", typeof(LoginRegisterPage));
            Routing.RegisterRoute("PaymentPage", typeof(PaymentPage));
            Routing.RegisterRoute("ProfilePage", typeof(ProfilePage));
            Routing.RegisterRoute("SalePage", typeof(SalePage));
            Routing.RegisterRoute("ScanningPage", typeof(ScanningPage));
            Routing.RegisterRoute("MyBasketPage", typeof(MyBasketPage));
            Routing.RegisterRoute("ReciptPage", typeof(ReceiptPage));
            Routing.RegisterRoute("MorePage", typeof(MorePage));
          
        }

        // Use OnAppearing to handle navigation after Shell is initialized
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // If the user is logged in, navigate directly to MainPage (TabBar with tabs)
            if (IsUserLoggedIn())
            {
                // Navigate to MainPage tab bar if logged in
                await Shell.Current.GoToAsync("///MainPage");
            }
            else { 
            // Registering the pages

           
                // Navigate to LoginRegisterPage if not logged in
                await Shell.Current.GoToAsync("///IntroPage");
            }
        }

        // This method checks if the user is logged in, return true if logged in
        private bool IsUserLoggedIn()
        {
            // Your logic to check if the user is logged in
            // For example, check a saved authentication token or user state.
            return false;  // Return true if user is logged in, false if not
        }

            
	}
}

