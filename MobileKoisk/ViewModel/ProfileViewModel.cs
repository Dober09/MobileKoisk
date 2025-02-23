using MobileKoisk.Services;
using MobileKoisk.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using MobileKoisk.Utils;
using CommunityToolkit.Mvvm.Input;

namespace MobileKoisk.ViewModel
{
    public partial class ProfileViewModel : ObservableObject
    {


        private readonly IPreferences _preferences;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private int userId;

        [ObservableProperty]
        private string userType;

        [ObservableProperty]
        private string smartCardNumber = "A20c0244"; // Example static data

        [ObservableProperty]
        private decimal totalSales = 300.00M; // Example static data

        [ObservableProperty]
        private int availableCoupons = 0; // Example static data

        public ProfileViewModel(IPreferences preferences)
        {
            _preferences = preferences;
            LoadUserData();
        }


        private void LoadUserData()
        {
            // Load user data from preferences
            Email = _preferences.Get(Constants.UserEmail, string.Empty);
            UserId = _preferences.Get(Constants.UserId, 0);
            UserType = _preferences.Get(Constants.UserType, string.Empty);

            // Load user name from preferences
            Name = _preferences.Get(Constants.UserName, string.Empty);

            // If name is empty but we have an email, use the email prefix as a fallback
            if (string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email))
            {
                Name = Email.Split('@')[0];
            }
        }


        [RelayCommand]
        private async Task Logout()
        {
            // Clear all preferences
            _preferences.Clear();

            // Navigate to login page
            await Shell.Current.GoToAsync("///LoginRegisterPage");
        }


    }

     
    
}
