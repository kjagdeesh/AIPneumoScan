using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPneumoScan.Converters
{
    /// <summary>
    /// Converts a selected theme index (0, 1, 2) into a corresponding image path.
    /// Used to display the correct image preview for the selected app theme.
    /// </summary>
    public class ThemeIndexToImageConverter : IValueConverter
    {
        /// <summary>
        /// Converts an integer theme index to an image file name.
        /// </summary>
        /// <param name="value">The theme index: 0 = Light, 1 = Dark, 2 = System (or any other).</param>
        /// <param name="targetType">The target type (ImageSource or string).</param>
        /// <param name="parameter">Optional parameter (not used).</param>
        /// <param name="culture">The culture info (not used).</param>
        /// <returns>A string representing the image filename based on the theme index.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int index)
            {
                return index switch
                {
                    0 => "theme_light.png",
                    1 => "theme_dark.png",
                    _ => "theme_system.png"
                };
            }

            return "theme_system.png";
        }

        /// <summary>
        /// ConvertBack is not implemented since reverse conversion is not needed.
        /// </summary>
        /// <param name="value">The value to convert back (not used).</param>
        /// <param name="targetType">The target type (not used).</param>
        /// <param name="parameter">Optional parameter (not used).</param>
        /// <param name="culture">The culture info (not used).</param>
        /// <returns>Always returns null.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
