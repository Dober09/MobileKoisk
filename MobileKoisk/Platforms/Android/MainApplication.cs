﻿using Android.App;
using Android.OS;
using Android.Runtime;

namespace MobileKoisk
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

 

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
