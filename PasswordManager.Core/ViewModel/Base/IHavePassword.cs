﻿
using System.Security;

namespace PasswordManager.Core {

    /// <summary>
    /// Interface for a class that can provide a secure password
    /// </summary>
    public interface IHavePassword {
        /// <summary>
        /// The secure password
        /// </summary>
        SecureString SecurePassword { get; }
    }
}
