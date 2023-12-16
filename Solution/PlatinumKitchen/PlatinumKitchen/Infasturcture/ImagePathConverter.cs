using PlatinumKitchen.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PlatinumKitchen.Infasturcture
{
    public class ImagePathConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSourceConverter imageConverter = new ImageSourceConverter();
            try
            {
                return imageConverter.ConvertFromString(Controller.Uri + $"/{values[1]}/{values[0]}.jpg");
            }
            catch (Exception)
            {
                return imageConverter.ConvertFromString(Controller.Uri + $"/{values[1]}/0.jpg");
            }

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
