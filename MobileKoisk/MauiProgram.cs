using Microsoft.Extensions.Logging;
using MobileKoisk.Services;
using MobileKoisk.View;
using MobileKoisk.ViewModel;
using Camera.MAUI;

namespace MobileKoisk
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCameraView()
                .ConfigureFonts(fonts =>
                {
             
                    //This is our new primary fonts
                    fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                    fonts.AddFont("Poppins-Light.ttf", "PoppinsLight");
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                    fonts.AddFont("Poppins-Thin.ttf", "PoppinsThin");
                });
            //navigation service
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddTransient<BasketPageViewModel>();

            //Register Pages 
            builder.Services.AddTransient<CartPage>();
			builder.Services.AddTransient<PaymentPage>();


#if DEBUG
			builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
