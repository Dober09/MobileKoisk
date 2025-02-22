using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileKoisk.Utils
{
    public class Constants
    {
        public const string AuthToken = "auth_token";
        public const string UserId = "user_id";
        public const string UserEmail = "user_email";
        public const string UserType = "user_type";

        // Add to Constants.cs
        public static bool IsAuthenticated =>
            !string.IsNullOrEmpty(Preferences.Default.Get(Constants.AuthToken, string.Empty));
    }
}
