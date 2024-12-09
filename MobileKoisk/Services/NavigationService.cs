using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileKoisk.Services
{
    public class NavigationService : INavigationService
    {
        public async Task NavigateToAsync<TPage>() where TPage : Page, new()
        {
            if (Application.Current.MainPage is NavigationPage navPage)
            {
                await navPage.PushAsync(new TPage());
            }
            else
            {
                throw new InvalidOperationException("error");
            }
        }
    }
    

}
