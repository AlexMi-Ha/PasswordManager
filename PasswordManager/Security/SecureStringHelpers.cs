using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PasswordManager {
    /// <summary>
    /// Helpers for the SecureString class
    /// </summary>
    public static class SecureStringHelpers {

        /// <summary>
        /// Unsecures a SecureString to plain text
        /// </summary>
        /// <param name="secureString"></param>
        /// <returns></returns>
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
