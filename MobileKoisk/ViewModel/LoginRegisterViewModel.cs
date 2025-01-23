using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading.Tasks;

namespace MobileKiosk.ViewModel
{
	public class LoginRegisterViewModel : INotifyPropertyChanged
	{
		private string _name;
		private string _surname;
		private string _email;
		private string _password;
		private string _confirmPassword;
		private bool _isLogin = true;
		private bool _isPasswordVisible;
		private string _userType = "Customer";
		private bool _isCustomer;
		private bool _isStoreHandler;

		public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		public string Surname
		{
			get => _surname;
			set => SetProperty(ref _surname, value);
		}

		public string Email
		{
			get => _email;
			set => SetProperty(ref _email, value);
		}

		public string Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}

		public string ConfirmPassword
		{
			get => _confirmPassword;
			set => SetProperty(ref _confirmPassword, value);
		}

		public bool IsLogin
		{
			get => _isLogin;
			set => SetProperty(ref _isLogin, value);
		}

		public bool IsPasswordVisible
		{
			get => _isPasswordVisible;
			set => SetProperty(ref _isPasswordVisible, value);
		}

		public string UserType
		{
			get => _userType;
			set => SetProperty(ref _userType, value);
		}

		public bool IsCustomer
		{
			get => _isCustomer;
			set
			{
				if (SetProperty(ref _isCustomer, value))
				{
					if (_isCustomer)
					{
						UserType = "Customer";
						IsStoreHandler = false;  // Ensure StoreHandler is false
					}
				}
			}
		}

		public bool IsStoreHandler
		{
			get => _isStoreHandler;
			set
			{
				if (SetProperty(ref _isStoreHandler, value))
				{
					if (_isStoreHandler)
					{
						UserType = "StoreHandler";
						IsCustomer = false;  // Ensure Customer is false
					}
				}
			}
		}

		public ICommand SetRoleCommand { get; }
		public ICommand GoogleLoginCommand { get; }
		public ICommand FacebookLoginCommand { get; }
		public ICommand ToggleLoginRegisterCommand { get; }
		public ICommand SubmitCommand { get; }
		public ICommand TogglePasswordVisibilityCommand { get; }

		public LoginRegisterViewModel()
		{
			SetRoleCommand = new Command<string>(SetRole);
			GoogleLoginCommand = new Command(OnGoogleLogin);
			FacebookLoginCommand = new Command(OnFacebookLogin);
			ToggleLoginRegisterCommand = new Command(ToggleLoginRegister);
			SubmitCommand = new Command(OnSubmit);
			TogglePasswordVisibilityCommand = new Command(TogglePasswordVisibility);
		}

		// Method to toggle password visibility
		private void TogglePasswordVisibility()
		{
			IsPasswordVisible = !IsPasswordVisible;
		}

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

		private void ToggleLoginRegister()
		{
			IsLogin = !IsLogin;
			Email = string.Empty;
			Password = string.Empty;
			ConfirmPassword = string.Empty;
		}

		private async void OnGoogleLogin()
		{
			await ShowAlert("Google Login", "Google login is not implemented yet.");
		}

		private async void OnFacebookLogin()
		{
			await ShowAlert("Facebook Login", "Facebook login is not implemented yet.");
		}

		private async void OnSubmit()
		{
			if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
			{
				await ShowAlert("Error", "Please fill in all fields");
				return;
			}

			if (!IsLogin)
			{
				if (Password != ConfirmPassword)
				{
					await ShowAlert("Error", "Passwords do not match");
					return;
				}

				bool registrationResult = await RegisterUser();
				if (registrationResult)
				{
					//await NavigateToMainPage();
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

		private async Task<bool> LoginUser()
		{

			await Shell.Current.GoToAsync("///MainPage");
			//bool isAuthenticated = Email == "test@example.com" && Password == "password";
			//if (!isAuthenticated)
			//{
			//    await ShowAlert("Login Failed", "Invalid email or password");
			//    return false;
			//}
			return true;
		}

		private async Task<bool> RegisterUser()
		{
			await ShowAlert("Registration", $"Registered as {UserType}");
			return true;
		}

		private async Task NavigateToMainPage()
		{
			await Shell.Current.GoToAsync("///MainPage");
		}

		private async Task ShowAlert(string title, string message)
		{
			await Application.Current.MainPage.DisplayAlert(title, message, "OK");
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return false;

			backingStore = value;
			OnPropertyChanged(propertyName);
			return true;
		}
	}
}
