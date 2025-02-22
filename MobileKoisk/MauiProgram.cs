using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;


using ZXing.Net.Maui.Controls;
using MobileKoisk.ViewModel;
using MobileKoisk.View;
using MobileKoisk.Services;
using MobileKiosk.ViewModel;




namespace MobileKoisk
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
			
			var builder = MauiApp.CreateBuilder();

                //cameraview
            builder
                .UseMauiApp<App>()
				.UseMauiCommunityToolkit()
                //cameraview
                
                .UseBarcodeReader()
                .ConfigureFonts(fonts =>
                {
                    //This is our new primary fonts
                    fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                    fonts.AddFont("Poppins-Light.ttf", "PoppinsLight");
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                    fonts.AddFont("Poppins-Thin.ttf", "PoppinsThin");
                })
                .ConfigureMauiHandlers(h=>
                {
#if ANDROID
                    h.AddHandler<Shell, TabbarBadgeRenderer>();
#endif
                });


         
            //Register services
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<IPreferences>(Preferences.Default);


            // Register your services

            builder.Services.AddSingleton<LoginRegisterPage>();
            builder.Services.AddSingleton<LoginRegisterViewModel>();

            builder.Services.AddSingleton<ProductItemService>();
			builder.Services.AddSingleton<WishListServices>();
            builder.Services.AddSingleton<SalesService>();

            builder.Services.AddTransient<ScanningViewModel>();
            builder.Services.AddTransient<ScanningPage>();
            builder.Services.AddTransient<ScannedPopup>();
    

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            builder.Services.AddSingleton<BasketPageViewModel>();
            builder.Services.AddSingleton<BadgeCounterService>();

			builder.Services.AddTransient<WishListPage>(serviceProvider =>
			new WishListPage(serviceProvider.GetRequiredService<WishListServices>()));

#if DEBUG
			builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
