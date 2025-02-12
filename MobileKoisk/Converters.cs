
using System.Globalization;

namespace MobileKoisk.Converters
{
    public class ByteArrayToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value is byte[] imageBytes)
            {
                return ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }

            if (value is string base64String)
            {
                try
                {
                    byte[] bytes = System.Convert.FromBase64String(base64String);
                    return ImageSource.FromStream(() => new MemoryStream(bytes));
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    // Converter string image to image
    public class Base64ImageSourceConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string base64String && !base64String.Equals("user.png"))
            {
                try
                {
                    byte[] imageBytes = System.Convert.FromBase64String(base64String);
                    return ImageSource.FromStream(() => new MemoryStream(imageBytes));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error converting base64 to image: {ex.Message}");
                }
            }
                    return "user.png";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // Convert bool to page title
    public class BoolToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "Login" : "Create An Account";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    // Convert bool to button text
    public class BoolToButtonTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "Login" : "Register";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // Convert bool to password visibility icon
    public class PasswordVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool b && b ? "eye.png" : "eye-off.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


   


    // Convert string to radio button checked state
    public class StringToRadioConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == parameter?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? parameter : null;
        }
    }

    // Toggle text for login/register switch
    public class LoginRegisterToggleTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isLogin = (bool)value;
            string param = parameter?.ToString();

            if (param == "Prompt")
            {
                return isLogin ? "Don't have an account? " : "Already have an account? ";
            }
            else if (param == "Action")
            {
                return isLogin ? "Register" : "Login";
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

	public class NullOrEmptyToBoolConverter : IValueConverter
	{
		// Convert: From string to bool
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// Check if the value is null or an empty string
			if (value is string str)
			{
				return string.IsNullOrEmpty(str);
			}

			// If the value is not a string, treat it as null/empty
			return true;
		}

		// ConvertBack: Not needed for this converter
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
