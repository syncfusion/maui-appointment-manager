using System.Globalization;

namespace ManageAppointments
{
    /// <summary>
    /// Converts appointment status strings to corresponding solid text color values.
    /// </summary>
    public class StatusToTextColorConverter : IValueConverter
    {
        /// <summary>
        /// Converts a status string ("Completed", "Pending", or "Missed") to a solid color for text.
        /// </summary>
        /// <param name="value">The status value to convert.</param>
        /// <param name="targetType">The target type (ignored).</param>
        /// <param name="parameter">Optional parameter (ignored).</param>
        /// <param name="culture">The culture info (ignored).</param>
        /// <returns>A Color representing the status, or gray/black as fallback.</returns>
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                return status switch
                {
                    "Completed" => Color.FromArgb("#116DF9"), // Dark blue
                    "Pending" => Color.FromArgb("#F4890B"),   // Dark orange
                    "Missed" => Color.FromArgb("#FF4E4E"),    // Dark red
                    _ => Colors.Gray
                };
            }

            return Colors.Black;
        }

        /// <summary>
        /// ConvertBack is not implemented for this converter.
        /// </summary>
        /// <param name="value">The value to convert back (not used).</param>
        /// <param name="targetType">The target binding type (not used).</param>
        /// <param name="parameter">Optional parameter (not used).</param>
        /// <param name="culture">The culture to use in the converter (not used).</param>
        /// <returns>Always returns null.</returns>
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}