using Ninject;
using PasswordManager.Core;
using System;
using System.Diagnostics;
using System.Globalization;

namespace PasswordManager {
    /// <summary>
    /// Converts a string name to a service pulled from the IoC
    /// </summary>
    public class IoCConverter : BaseValueConverter<IoCConverter> {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            switch((string)parameter) {

                case nameof(ApplicationViewModel):
                    return IoC.Get<ApplicationViewModel>();

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
