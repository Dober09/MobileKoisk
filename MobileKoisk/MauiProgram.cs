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



            // Register your services

            builder.Services.AddSingleton<LoginRegisterPage>();
            builder.Services.AddSingleton<LoginRegisterViewModel>();

            builder.Services.AddSingleton<ProductItemService>();

            builder.Services.AddTransient<ScanningViewModel>();
            builder.Services.AddTransient<ScanningPage>(); 

#if DEBUG
			builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
