using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PasswordManager.Core.Security {
    public static class SecureStringHelpers {

        public static string Unsecure(this SecureString secureString) {
            if (secureString == null)
                return string.Empty;

            // get a pointer for an unsecure string in memory
            var unmanagedString = IntPtr.Zero;
            try {
                // unsecures the string
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);

            }finally {
                // Clean up
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
