﻿using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;


using ZXing.Net.Maui.Controls;
using MobileKoisk.ViewModel;
using MobileKoisk.View;
using MobileKoisk.Services;

namespace MobileKoisk
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
			
			var builder = MauiApp.CreateBuilder();

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
                });

            // Register your services
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
