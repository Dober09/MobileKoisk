using MobileKoisk.Models;
using MobileKoisk.View;
using System.Collections.ObjectModel;

using System.Windows.Input;

namespace MobileKoisk.ViewModel
{
    public class IntroScreenViewModel : BaseViewModel
    {

        #region Property
        private int _position;

        private string _buttonText = "Next";

        public string ButtonText
        {
            get => _position == IntroScreens.Count - 1 ? "Start" : "Next";
            set => SetProperty(ref _buttonText, value);
        }

        public int Position
        {

            get => _position;
            set
            {
                if (SetProperty(ref _position, value))
                {
                    OnPropertyChanged(nameof(ButtonText));
                    System.Diagnostics.Debug.WriteLine($"Position changed to: {value}");
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
                    Position += 1; // Increment Position
                }
                else
                {
                    // Navigate to MainPage if last item
                    await AppShell.Current.GoToAsync($"//{nameof(MainPage)}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in NextCommand: {ex}");
            }
        });
    }
}
