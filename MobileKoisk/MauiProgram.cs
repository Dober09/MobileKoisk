using Microsoft.Extensions.Logging;


using Camera.MAUI;

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



#if DEBUG
			builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
