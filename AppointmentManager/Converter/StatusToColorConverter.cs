using System.Globalization;

namespace ManageAppointments
{
    /// <summary>
    /// Converts appointment status strings (e.g., "Pending", "Completed", "Missed") to corresponding semi-transparent color values.
    /// </summary>
    public class StatusToColorConverter : IValueConverter
    {
        /// <summary>
        /// Converts a status string to a corresponding Color.
        /// </summary>
        /// <param name="value">The status string.</param>
        /// <param name="targetType">The target binding type.</param>
        /// <param name="parameter">Optional parameter.</param>
        /// <param name="culture">The culture to use in the converter (ignored).</param>
        /// <returns>A Color value corresponding to the status.</returns>
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            switch (value as string)
            {
                case "Pending":
                    return Color.FromArgb("#10F4890B"); // Light green
                case "Completed":
                    return Color.FromArgb("#10116DF9"); // Light blue
                case "Missed":
                    return Color.FromArgb("#10FF4E4E"); // Light red
                default:
                    return Colors.Transparent;
            }
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
