using PlatinumKitchen.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
                if (File.Exists(Controller.Uri + $"/{values[1]}/Pre{values[0]}.jpg"))
                {
                    File.Copy(Controller.Uri + $"/{values[1]}/Pre{values[0]}.jpg", Controller.Uri + $"/{values[1]}/{values[0]}.jpg", true);
                    File.Delete(Controller.Uri + $"/{values[1]}/Pre{values[0]}.jpg");
                }
                return imageConverter.ConvertFromString(Controller.Uri + $"/{values[1]}/{values[0]}.jpg");
            }
            catch (Exception)
            {
                return imageConverter.ConvertFromString(Controller.Uri + $"/{values[1]}/0.jpg");
            }

        }

    private bool IsFileLocked(FileInfo file)
    {
        FileStream stream = null;

        try
        {
            stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
        }
        catch (IOException)
        {
            // Файл занят другим процессом
            return true;
        }
        finally
        {
            stream?.Close();
        }

        // Файл свободен
        return false;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
