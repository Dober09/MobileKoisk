using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileKoisk.Services
{
    public interface INavigationService
    {
       Task NavigateToAsync<TPage>() where TPage : Page, new();    
    }
}
