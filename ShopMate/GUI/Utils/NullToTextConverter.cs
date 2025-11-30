using Microsoft.UI.Xaml.Data;
using System;

namespace ShopMate.GUI
{
    public class NullToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null ? parameter?.ToString() ?? "N/A" : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class ExpiryDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return "No expiry";

            if (value is DateTime dt)
                return $"Expiry: {dt:dd MMM yyyy}";

            return "No expiry";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}
