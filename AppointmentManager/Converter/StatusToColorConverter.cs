using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace ManageAppointments
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            switch (value as string)
            {
                case "Pending":
                    return Color.FromArgb("#FFEBB5"); // Even lighter orange
                case "Completed":
                    return Color.FromArgb("#C8E6C9"); // Even lighter green
                case "Missed":
                    return Color.FromArgb("#FFEBEE"); // Even lighter red
                default:
                    return Colors.Transparent;
            }
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
