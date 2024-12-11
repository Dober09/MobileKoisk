using MobileKoisk.Helper;
using MobileKoisk.View;

namespace MobileKoisk
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Registering the pages
            Routing.RegisterRoute("paymentpage", typeof(PaymentPage));

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
            {
#if __ANDROID__
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.AntiqueWhite);
#endif
            });

            
		}
    }
}
