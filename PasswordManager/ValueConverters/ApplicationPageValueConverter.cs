using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace PasswordManager {
    /// <summary>
    /// Converts <see cref="ApplicationPage"/> to an actual view page
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter> {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            switch((ApplicationPage)value) {
                case ApplicationPage.MainPage:
                    return new MainPage();
                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
