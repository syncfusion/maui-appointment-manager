using System.Globalization;

namespace ManageAppointments
{
    public class TimeSpanToTextConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is TimeSpan time)
            {
                // Use custom format as needed
                return DateTime.Today.Add(time).ToString("hh:mm tt", culture);
            }
            else if (value is DateTime dateTime)
            {
                // Format DateTime as date (e.g., 06/07/2002)
                return dateTime.ToString("MM/dd/yyyy", culture);
            }

            return string.Empty;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                // Try parse as date first
                if (DateTime.TryParseExact(str, "MM/dd/yyyy", culture, DateTimeStyles.None, out DateTime dateResult))
                {
                    return dateResult;
                }

                // Try parse as time
                if (DateTime.TryParse(str, culture, DateTimeStyles.None, out DateTime timeResult))
                {
                    return timeResult.TimeOfDay;
                }
            }

            return targetType == typeof(DateTime) ? DateTime.MinValue : TimeSpan.Zero;
        }
    }
}
