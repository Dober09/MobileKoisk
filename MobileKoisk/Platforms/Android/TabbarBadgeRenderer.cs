
using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;

namespace MobileKoisk
{
    public class TabbarBadgeRenderer : ShellRenderer
    {

        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            //return base.CreateBottomNavViewAppearanceTracker(shellItem);
            return  new BadgeShellBottomNavViewAppearanceTracker(this, shellItem);
        }

    }

    class BadgeShellBottomNavViewAppearanceTracker : ShellBottomNavViewAppearanceTracker
    {
        public BadgeShellBottomNavViewAppearanceTracker(IShellContext shellContext, ShellItem shellItem) : base(shellContext, shellItem)
        {
        }

        public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            base.SetAppearance(bottomView, appearance);
            const int basketIdx = 3;
            const int wishIdx = 1;

            var  badgeDrawable = bottomView.GetOrCreateBadge(basketIdx);
            badgeDrawable.Number = 10;
            badgeDrawable.BackgroundColor = Colors.Red.ToPlatform();
            badgeDrawable.BadgeTextColor = Colors.White.ToPlatform();

           var badgeDrawable2 = bottomView.GetOrCreateBadge(wishIdx);
           badgeDrawable2.Number = 5;
           badgeDrawable2.BackgroundColor = Colors.Red.ToPlatform();
           badgeDrawable2.BadgeTextColor = Colors.White.ToPlatform();

            
        }
    }
}
