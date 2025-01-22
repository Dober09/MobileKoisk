
using Google.Android.Material.Badge;
using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using MobileKoisk.Services;

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
        private BadgeDrawable? badgeDrawable;
        public BadgeShellBottomNavViewAppearanceTracker(IShellContext shellContext, ShellItem shellItem) : base(shellContext, shellItem)
        {
        }

        public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            base.SetAppearance(bottomView, appearance);
            if (badgeDrawable is null)
            {

                const int basketIdx = 3;

                badgeDrawable = bottomView.GetOrCreateBadge(basketIdx);

                UpdateBadge(0);
                BadgeCounterService.CountChanged += OnCountChanged;

              ;
            }

            const int wishIdx = 1;
           var badgeDrawable2 = bottomView.GetOrCreateBadge(wishIdx);
           badgeDrawable2.Number = 5;
           badgeDrawable2.BackgroundColor = Colors.Red.ToPlatform();
           badgeDrawable2.BadgeTextColor = Colors.White.ToPlatform();

            
        }

        private void OnCountChanged(object? sender,int newCount)
        {
           UpdateBadge(newCount);
        }

        private void UpdateBadge(int count)
        {
            if (badgeDrawable is not null)
            {
                if (count <= 0)
                {
                    badgeDrawable.SetVisible(false);
                }
                else {
                    badgeDrawable.Number = count;
                    badgeDrawable.BackgroundColor = Colors.Red.ToPlatform();
                    badgeDrawable.BadgeTextColor = Colors.White.ToPlatform();
                    badgeDrawable.SetVisible(true);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            BadgeCounterService.CountChanged -= OnCountChanged;
        }
    }
}
