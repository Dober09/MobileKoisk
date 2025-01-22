using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MobileKoisk.Helper
{
	internal class BoolToColorConverter : IValueConverter
	{


		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool isSelected)
			{
				return isSelected ? Color.FromArgb("#FFD700") : Color.FromArgb("#D3D3D3"); // Selected or default color
			}
			return Color.FromArgb("#D3D3D3"); // Default color
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
