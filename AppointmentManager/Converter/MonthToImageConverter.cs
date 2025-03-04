using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace ManageAppointments
{
    /// <summary>
    /// Converter used to convert a month value into an image source for the agenda view month header.
    /// </summary>
    internal class MonthToImageConverter : IValueConverter
    {
        /// <summary>
        /// Converts a month value into an image source.
        /// </summary>
        /// <param name="value">The month value to be converted.</param>
        /// <param name="targetType">The target binding type (not used).</param>
        /// <param name="parameter">Optional parameter (not used).</param>
        /// <param name="culture">The culture information.</param>
        /// <returns>
        /// An <see cref="ImageSource"/> corresponding to the month name (e.g., "january.png").
        /// Returns null if the value is null.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var monthName = String.Format("{0:MMMM}", value).ToLower() + ".png";
                return ImageSource.FromFile(monthName);
            }

            return null;
        }

        /// <summary>
        /// ConvertBack is not implemented as it's not needed for this converter.
        /// </summary>
        /// <param name="value">The value to be converted back (not used).</param>
        /// <param name="targetType">The target type (not used).</param>
        /// <param name="parameter">Optional parameter (not used).</param>
        /// <param name="culture">The culture information.</param>
        /// <returns>Always returns null.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
