using System;
using System.Windows.Data;

namespace DotNetBay.MVVM.Converter
{
    public class BooleanToStatusTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((Boolean)value)
            {
                case true:
                    return "Abgeschlossen";
                case false:
                    return "Offen";
            }
            return "Offen";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is String)
            {
                if (value.ToString().ToLower() == "Abgeschlossen")
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}
