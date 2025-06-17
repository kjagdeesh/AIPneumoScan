using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPneumoScan.Converters
{
    /// <summary>
    /// Converts between an integer (such as a selected index) and a boolean value for use in UI bindings.
    /// Useful for RadioButton or SegmentedControl group selection.
    /// </summary>
    public class IntToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Converts an integer value to a boolean by comparing it with a parameter.
        /// Returns true if the integer equals the parameter value.
        /// </summary>
        /// <param name="value">The source value (expected to be int).</param>
        /// <param name="targetType">The target type (expected to be bool).</param>
        /// <param name="parameter">The comparison value (passed as a string but should be an int).</param>
        /// <param name="culture">The culture info (not used).</param>
        /// <returns>True if the value equals the parameter, otherwise false.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int selectedIndex && parameter is string paramStr && int.TryParse(paramStr, out int paramVal))
                return selectedIndex == paramVal;

            return false;
        }

        /// <summary>
        /// Converts a boolean value back to an integer if the value is true.
        /// This is typically used when a RadioButton or toggle is selected.
        /// </summary>
        /// <param name="value">The boolean value indicating selection.</param>
        /// <param name="targetType">The target type (expected to be int).</param>
        /// <param name="parameter">The value to return if the boolean is true.</param>
        /// <param name="culture">The culture info (not used).</param>
        /// <returns>The integer value from the parameter if value is true; otherwise, Binding.DoNothing.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked && int.TryParse(parameter?.ToString(), out int index))
                return index;

            return Binding.DoNothing;
        }
    }
}
