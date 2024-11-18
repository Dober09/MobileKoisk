using MobileKoisk.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileKoisk.ViewModel
{
    public class IntroScreenViewModel
    {

        #region Property
        public ObservableCollection<IntroScreenModel> IntroScreens { get; set; } = new ObservableCollection<IntroScreenModel>();
        #endregion

        

        public IntroScreenViewModel() {

            IntroScreens.Add(new IntroScreenModel
            {
                IntroTitle = "Welcome to Mobile Koisk",
                IntroDescription = "Skip the line and shop smarter",
                IntroImage = "basketblack.png",
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
    }
}
