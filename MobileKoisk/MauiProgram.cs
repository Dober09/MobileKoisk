using Microsoft.Extensions.Logging;


using Camera.MAUI;
using MobileKiosk.ViewModel;
using MobileKoisk.View;

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
				
                .UseMauiCameraView()
                .ConfigureFonts(fonts =>
                {
                    //This is our new primary fonts
                    fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                    fonts.AddFont("Poppins-Light.ttf", "PoppinsLight");
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                    fonts.AddFont("Poppins-Thin.ttf", "PoppinsThin");
                });
            // Register ViewModels and Views
                  builder.Services.AddTransient<LoginRegisterViewModel>();
                  builder.Services.AddTransient<LoginRegisterPage>();
                  builder.Services.AddTransient<MainPage>();
                  builder.Services.AddSingleton<AppShell>();
#if DEBUG
			builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
