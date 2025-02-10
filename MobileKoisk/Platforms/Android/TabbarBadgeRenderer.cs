
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
        private BadgeDrawable? basketBadgeDrawable;
        private BadgeDrawable? wishBadge;
        public BadgeShellBottomNavViewAppearanceTracker(IShellContext shellContext, ShellItem shellItem) : base(shellContext, shellItem)
        {
        }

        public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            base.SetAppearance(bottomView, appearance);
            // Basket Badge
            if (basketBadgeDrawable is null)
            {
                const int basketIdx = 3;
                basketBadgeDrawable = bottomView.GetOrCreateBadge(basketIdx);
                UpdateBasketBadge(BadgeCounterService.Count);
                BadgeCounterService.CountChanged += OnBasketCountChanged;
            }

            // Wishlist Badge
            if (wishBadge is null)
            {
                const int wishIdx = 1;
                wishBadge = bottomView.GetOrCreateBadge(wishIdx);
                UpdateWishlistBadge(WishlistCounterServirce.Count);
                WishlistCounterServirce.CountChanged += OnWishlistCountChanged;
            }


        }

        private void OnBasketCountChanged(object? sender, int newCount)
        {
            UpdateBasketBadge(newCount);
        }

        private void OnWishlistCountChanged(object? sender, int newCount)
        {
            UpdateWishlistBadge(newCount);
        }

        private void UpdateBasketBadge(int count)
        {
            if (basketBadgeDrawable is not null)
            {
                if (count <= 0)
                {
                    basketBadgeDrawable.SetVisible(false);
                }
                else
                {
                    basketBadgeDrawable.Number = count;
                    basketBadgeDrawable.BackgroundColor = Colors.Red.ToPlatform();
                    basketBadgeDrawable.BadgeTextColor = Colors.White.ToPlatform();
                    basketBadgeDrawable.SetVisible(true);
                }
            }
        }

        private void UpdateWishlistBadge(int count)
        {
            if (wishBadge is not null)
            {
                if (count <= 0)
                {
                    wishBadge.SetVisible(false);
                }
                else
                {
                    wishBadge.Number = count;
                    wishBadge.BackgroundColor = Colors.Red.ToPlatform();
                    wishBadge.BadgeTextColor = Colors.White.ToPlatform();
                    wishBadge.SetVisible(true);
                }
            }
        }



        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            BadgeCounterService.CountChanged -= OnBasketCountChanged;
            WishlistCounterServirce.CountChanged -= OnWishlistCountChanged;
        }
    }

 
}

