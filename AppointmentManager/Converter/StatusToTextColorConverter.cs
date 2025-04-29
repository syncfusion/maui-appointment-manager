using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace ManageAppointments
{
    public class StatusToTextColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                return status switch
                {
                    "Completed" => Color.FromArgb("#006400"), // Dark green
                    "Pending" => Color.FromArgb("#CC6600"),   // Dark orange
                    "Missed" => Color.FromArgb("#B22222"),    // Dark red
                    _ => Colors.Gray
                };
            }
            return Colors.Black;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
