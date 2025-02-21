using MobileKiosk.ViewModel;

namespace MobileKoisk.View;

public partial class LoginRegisterPage : ContentPage
{
    public LoginRegisterViewModel ViewModel { get; private set; }

    public LoginRegisterPage(LoginRegisterViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        BindingContext = ViewModel;
    }

    // Event handler for UserType RadioButton changes
    private void OnUserTypeRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value && sender is RadioButton radioButton)
        {
            // Update the UserType property in the ViewModel
            ViewModel.UserType = radioButton.Content?.ToString();
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        bool isAdult = await DisplayAlert("Age Verification",
            "Are you 18 years or older?",
            "Yes", "No");

        if (isAdult)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
        else
        {
            await DisplayAlert("Access Denied",
                "We apologize, but you must be 18 or older to use this application. Please return when you meet the age requirement.",
                "OK");
        }
    }
}
