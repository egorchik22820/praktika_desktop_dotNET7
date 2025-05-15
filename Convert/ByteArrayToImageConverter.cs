using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace praktika_desktop_dotNET7.Convert
{
    public class ByteArrayToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] imageData)
            {
                var imageSource = new BitmapImage();
                using (var stream = new MemoryStream(imageData))
                {
                    imageSource.BeginInit();
                    imageSource.CacheOption = BitmapCacheOption.OnLoad;
                    imageSource.StreamSource = stream;
                    imageSource.EndInit();
                }
                return imageSource;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}