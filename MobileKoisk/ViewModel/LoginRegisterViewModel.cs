using CommunityToolkit.Mvvm.ComponentModel;
using MobileKoisk.Models;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;



namespace MobileKiosk.ViewModel
{
    public partial class LoginRegisterViewModel : ObservableObject
    {

       

        [ObservableProperty]
        private DateTime dateOfBirth = DateTime.Today;

        [ObservableProperty]
        private string phoneNumber;

   
        // Mock database of users
        private static readonly List<UserData> _users = new();
        [ObservableProperty]
        private bool acceptedTerms;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string surname;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string confirmPassword;

        [ObservableProperty]
        private bool isLogin = true;

        [ObservableProperty]
        private bool isPasswordVisible;

        [ObservableProperty]
        private string userType = "Customer";

        [ObservableProperty]
        private string passwordIcon = "eye.png";

        private bool isCustomer;
        private bool isStoreHandler;

        public bool IsCustomer
        {
            get => isCustomer;
            set
            {
                if (SetProperty(ref isCustomer, value) && isCustomer)
                {
                    UserType = "Customer";
                    IsStoreHandler = false;
                }
            }
        }

        public bool IsStoreHandler
        {
            get => isStoreHandler;
            set
            {
                if (SetProperty(ref isStoreHandler, value) && isStoreHandler)
                {
                    UserType = "StoreHandler";
                    IsCustomer = false;
                }
            }
        }

        public LoginRegisterViewModel()
        {
            // Add test user if empty
            if (!_users.Any())
            {
                _users.Add(new UserData
                {
                    Email = "test@example.com",
                    Password = "password123",
                    Name = "Test",
                    Surname = "User",
                    UserType = "Customer"
                });
            }
        }

        [RelayCommand]
        private void SetRole(string role)
        {
            if (role == "Customer")
            {
                IsCustomer = true;
                IsStoreHandler = false;
            }
            else if (role == "StoreHandler")
            {
                IsStoreHandler = true;
                IsCustomer = false;
            }
        }

        [RelayCommand]
        private async Task GoogleLogin()
        {
            await ShowAlert("Google Login", "Google login is not implemented yet.");
        }

        [RelayCommand]
        private async Task FacebookLogin()
        {
            await ShowAlert("Facebook Login", "Facebook login is not implemented yet.");
        }

        [RelayCommand]
        private void ToggleLoginRegister()
        {
            IsLogin = !IsLogin;
            ClearFields();
        }

        [RelayCommand]
        private async Task Submit()
        {

            if (!IsLogin && !AcceptedTerms)
            {
                await ShowAlert("Error", "Please accept the terms and conditions");
                return;
            }
            if (!ValidateEmail(Email))
            {
                await ShowAlert("Error", "Please enter a valid email address");
                return;
            }

            if (!ValidatePassword(Password))
            {
                await ShowAlert("Error", "Password must be at least 8 characters long and contain at least one number");
                return;
            }

            if (!IsLogin)
            {
                if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Surname))
                {
                    await ShowAlert("Error", "Please fill in all fields");
                    return;
                }

                if (!ValidatePhoneNumber(PhoneNumber))
                {
                    await ShowAlert("Error", "Please enter a valid phone number");
                    return;
                }

                var age = CalculateAge(DateOfBirth);
                if (age < 18)
                {
                    await ShowAlert("Error", "You must be 18 or older to register");
                    return;
                }
                if (Password != ConfirmPassword)
                {
                    await ShowAlert("Error", "Passwords do not match");
                    return;
                }

                bool registrationResult = await RegisterUser();
                if (registrationResult)
                {
                    await Shell.Current.GoToAsync("///MainPage");
                }
            }
            else
            {
                bool loginResult = await LoginUser();
                if (loginResult)
                {
                    await Shell.Current.GoToAsync("///MainPage");
                }
            }
        }


        private bool ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber)) return false;
            return Regex.IsMatch(phoneNumber, @"^\+?[\d\s-]{10,}$");
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }


        [RelayCommand]
        private void TogglePasswordVisibility()
        {
            IsPasswordVisible = !IsPasswordVisible;
            PasswordIcon = IsPasswordVisible ? "openeye.png" : "eye.png";
        }

        private void ClearFields()
        {
            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            Name = string.Empty;
            Surname = string.Empty;
            PhoneNumber = string.Empty;
            DateOfBirth = DateTime.Today;
        }

        private bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;
            return password.Length >= 8 && password.Any(char.IsDigit);
        }

        private async Task<bool> LoginUser()
        {
            var user = _users.FirstOrDefault(u => u.Email == Email && u.Password == Password);

            if (user == null)
            {
                await ShowAlert("Login Failed", "Invalid email or password");
                return false;
            }

            UserType = user.UserType;
            return true;
        }

        private async Task<bool> RegisterUser()
        {
            if (_users.Any(u => u.Email == Email))
            {
                await ShowAlert("Registration Failed", "An account with this email already exists");
                return false;
            }

            _users.Add(new UserData
            {
                Name = Name,
                Surname = Surname,
                Email = Email,
                Password = Password,
                UserType = UserType
            });

            await ShowAlert("Registration Successful", $"Registered as {UserType}");
            return true;
        }

        private async Task ShowAlert(string title, string message)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }


    }
}
