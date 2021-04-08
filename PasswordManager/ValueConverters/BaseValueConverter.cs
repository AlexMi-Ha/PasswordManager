using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace PasswordManager {
    /// <summary>
    /// A Base class for all ValueConverters
    /// </summary>
    /// <typeparam name="T">Type of this ValueConverter</typeparam>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter where T : class, new() {

        #region Private Attributes
        private static T converter = null;

        #endregion

        #region Markup Extension Methods
        /// <summary>
        /// Provides a static instance of the value converter
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return converter ??= new T();
        }
        #endregion

        #region Value Converter Methods

        /// <summary>
        /// Method to convert a type to another
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Method to convert a value back to the original type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        #endregion
    }
}
