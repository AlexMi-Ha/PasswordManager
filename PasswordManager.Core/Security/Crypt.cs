using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.Core {
    /// <summary>
    /// This class contains helpers to handle Encryption, Decryption and Hashing or to generate random Passwords
    /// </summary>
    public static class Crypt {

        /// <summary>
        /// Encrypt a String using a Key
        /// </summary>
        /// <param name="key">Key used for the symmetric encryption</param>
        /// <param name="plainText">PlainText to encrypt</param>
        /// <returns></returns>
        public static string EncryptString(string key, string plainText) {
            byte[] iv = new byte[16];
            byte[] encryptArray;

            using (Aes aes = Aes.Create()) {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using(MemoryStream memoryStream = new MemoryStream()) {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write)) {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream)) {
                            streamWriter.Write(plainText);
                        }

                        encryptArray = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encryptArray);
        }

        /// <summary>
        /// Decrypt a String using a Key
        /// </summary>
        /// <param name="key">Key used to the symmetric decryption</param>
        /// <param name="cipherText">Ciphertext to decrypt</param>
        /// <returns></returns>
        public static string DecryptString(string key, string cipherText) {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create()) {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer)) {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read)) {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream)) {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Hashing a string
        /// </summary>
        /// <param name="plainText">PlainText to hash</param>
        /// <returns></returns>
        public static string Hash(string plainText) {
            using (var crypt = MD5.Create()) {
                byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                return Convert.ToBase64String(crypto);
            }
        }



        public static string GeneratePassword(bool includeLowecase, bool includeUppercase, bool includeNum, bool includeSpecial, bool disableTwoIdenticalsInARow, int minLengthOfPassword, int maxLengthOfPassword) {
            const string LOWERCASE_CHARACTERS = "abcdefghijklmnopqrstuvwxyz";
            const string UPPERCASE_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string NUMERIC_CHARACTERS = "0123456789";
            const string SPECIAL_CHARACTERS = "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
            const int PASSWORD_LENGTH_MIN = 8;
            const int PASSWORD_LENGTH_MAX = 128;

            if(minLengthOfPassword < PASSWORD_LENGTH_MIN || maxLengthOfPassword > PASSWORD_LENGTH_MAX) {
                // TODO throw error
                return null;
            }

            string characterSet = "";
            if (includeLowecase) characterSet += LOWERCASE_CHARACTERS;
            if (includeUppercase) characterSet += UPPERCASE_CHARACTERS;
            if (includeNum) characterSet += NUMERIC_CHARACTERS + NUMERIC_CHARACTERS;
            if (includeSpecial) characterSet += SPECIAL_CHARACTERS;

            Random random = new Random();

            char[] password = new char[random.Next(minLengthOfPassword, maxLengthOfPassword)];
            int charSetLength = characterSet.Length;

            for(int characterPos = 0; characterPos < password.Length; ++characterPos) {
                password[characterPos] = characterSet[random.Next(charSetLength)];

                if (disableTwoIdenticalsInARow) {

                    bool moreThanTwoIdenticalsInARow = characterPos > 2
                        && password[characterPos] == password[characterPos - 1]
                        && password[characterPos - 1] == password[characterPos - 2];

                    if (moreThanTwoIdenticalsInARow)
                        characterPos--;
                }
            }
            return string.Join(null, password);
        }

    }
}
