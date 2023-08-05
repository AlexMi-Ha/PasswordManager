using System;

namespace PasswordManager.Core.DI {
    public static class DICollection {

        private static IServiceProvider _serviceProvider;

        public static void Setup(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public static T Resolve<T>() {
            return (T)_serviceProvider.GetService(typeof(T));
        }
    }
}
