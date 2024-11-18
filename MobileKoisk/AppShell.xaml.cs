﻿using MobileKoisk.Helper;
namespace MobileKoisk
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
            {
#if __ANDROID__ 
             handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.AntiqueWhite);
#endif
            });
            
		}
    }
}
