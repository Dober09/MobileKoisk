using MobileKoisk.Models;
using MobileKoisk.View;
using System.Collections.ObjectModel;
using System.Diagnostics;

using System.Windows.Input;

namespace MobileKoisk.ViewModel
{
    public class IntroScreenViewModel : BaseViewModel
    {

        #region Property
        private int position;

        private string _buttonText = "Next";

       public string ButtonText
        {
            get => position == IntroScreens.Count - 1 ? "Start" : "Next";
            set => SetProperty(ref _buttonText, value);
        }

        public int Position
        {

            get => position;
            set
            {
                if (SetProperty(ref position, value))
                {
                    OnPropertyChanged(ButtonText);
                    Debug.WriteLine($"Position changed to: {value}");
                }
            }
        }
        public ObservableCollection<IntroScreenModel> IntroScreens { get; set; } = new ObservableCollection<IntroScreenModel>();
        #endregion

        

        public IntroScreenViewModel() {
            

            IntroScreens.Add(new IntroScreenModel
            {
                IntroTitle = "Welcome to Mobile Koisk",
                IntroDescription = "Skip the line and shop smarter",
                IntroImage = "clearbasket.png",
            }); 
            
            IntroScreens.Add(new IntroScreenModel
            {
                IntroTitle = "Scan & Shop ",
                IntroDescription = "Simply scan barcodes to check prices and add items to your shopping list instantly",
                IntroImage = "scan.png",
            });  
            
            
            IntroScreens.Add(new IntroScreenModel
            {
                IntroTitle = "Quick & Secure Payment",
                IntroDescription = "Skip the queue! Pay directly through the app and enjoy the shopping experience",
                IntroImage = "payment.png",
            });
            
        }


        public ICommand NextCommand => new Command(async () =>
        {

            try
            {
                if (Position <= IntroScreens.Count - 1)
                {
                    Position++; // Increment Position
                }
                else
                {
                    // Navigate to MainPage if last item
                    await CompleteOnboarding();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in NextCommand: {ex}");
            }
        });

        public ICommand SkipCommand => new Command(async () => await CompleteOnboarding());

        private async Task CompleteOnboarding()

        {
            try
            {

                Preferences.Set("OnboandingComplete", true);

                await Shell.Current.GoToAsync("//MainPage");
            }
            catch (Exception ex)
            {
                // Log the error for debugging purposes
                Debug.WriteLine($"Error during onboarding completion: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to complete onboarding.", "OK");
            }
        }
    }
}
