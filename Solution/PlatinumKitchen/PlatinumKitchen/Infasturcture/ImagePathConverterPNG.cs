using PlatinumKitchen.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PlatinumKitchen.Infasturcture
{
    public class ImagePathConverterPNG : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSourceConverter imageConverter = new ImageSourceConverter();
            try
            {
                if (File.Exists(Controller.Uri + $"/{values[1]}/Pree{values[0]}.png"))
                {
                    File.Copy(Controller.Uri + $"/{values[1]}/Pree{values[0]}.png", Controller.Uri + $"/{values[1]}/e{values[0]}.png", true);
                    File.Delete(Controller.Uri + $"/{values[1]}/Pree{values[0]}.png");
                }
                if (File.Exists(Controller.Uri + $"/{values[1]}/Pre{values[0]}.png"))
                {
                    File.Copy(Controller.Uri + $"/{values[1]}/Pre{values[0]}.png", Controller.Uri + $"/{values[1]}/{values[0]}.png", true);
                    File.Delete(Controller.Uri + $"/{values[1]}/Pre{values[0]}.png");
                }

                if (Controller.Admin)
                {
                    return imageConverter.ConvertFromString(Controller.Uri + $"/{values[1]}/admin.png");
                }
                else if (Controller.UserE != null)
                {
                    return imageConverter.ConvertFromString(Controller.Uri + $"/{values[1]}/e{values[0]}.png");
                }
                else 
                {
                    return imageConverter.ConvertFromString(Controller.Uri + $"/{values[1]}/{values[0]}.png");
                }
            }
            catch (Exception)
            {
                if (Controller.UserE != null)
                {
                    return imageConverter.ConvertFromString(Controller.Uri + $"/{values[1]}/e0.png");
                }
                else if (Controller.User != null)
                {
                    return imageConverter.ConvertFromString(Controller.Uri + $"/{values[1]}/0.png");
                }
                else
                {
                    return imageConverter.ConvertFromString(Controller.Uri + $"/{values[1]}/admin.png");
                }
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
