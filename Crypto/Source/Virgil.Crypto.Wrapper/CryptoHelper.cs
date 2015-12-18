namespace Virgil.Crypto.Wrapper
{
    public class CryptoHelper
    {
        /// <summary>
        /// Encrypts text for recipient's public key.
        /// </summary>
        /// <param name="text">The text to be encrypted</param>
        /// <param name="recipientId">The recipient's unique identifier.</param>
        /// <param name="recipientPublicKey">The recipient's public key data.</param>
        /// <returns>Encrypted text in Base64 format</returns>
        public static string Encrypt(string text, string recipientId, byte[] recipientPublicKey)
        {
            var textData = System.Text.Encoding.UTF8.GetBytes(text);

            var recipients = new System.Collections.Generic.Dictionary<string, byte[]> { { recipientId, recipientPublicKey } };
            var cipherDataBase64 = System.Convert.ToBase64String(Encrypt(textData, recipients));
            return cipherDataBase64;
        }

        /// <summary>
        /// Encrypts text for multiple recipient.
        /// </summary>
        /// <param name="text">The text to be encrypted</param>
        /// <param name="recipients">Dictionary of recipients public keys</param>
        /// <returns>Encrypted text in Base64 format</returns>
        public static string Encrypt(string text, System.Collections.Generic.IDictionary<string, byte[]> recipients)
        {
            var textData = System.Text.Encoding.UTF8.GetBytes(text);
            var cipherDataBase64 = System.Convert.ToBase64String(Encrypt(textData, recipients));
            return cipherDataBase64;
        }

        /// <summary>
        /// Encrypts text with password.
        /// </summary>
        /// <param name="text">The text to be encrypted</param>
        /// <param name="password">The password</param>
        /// <returns>Encrypted text in Base64 format</returns>
        public static string Encrypt(string text, string password)
        {
            var textData = System.Text.Encoding.UTF8.GetBytes(text);
            var cipherDataBase64 = System.Convert.ToBase64String(Encrypt(textData, password));
            return cipherDataBase64;
        }

        /// <summary>
        /// Encrypts text for multiple recipient.
        /// </summary>
        /// <param name="data">The data to be encrypted</param>
        /// <param name="recipients">Dictionary of recipients public keys</param>
        /// <returns>Encrypted data</returns>
        public static byte[] Encrypt(byte[] data, System.Collections.Generic.IDictionary<string, byte[]> recipients)
        {
            using (var cipher = new VirgilCipher())
            {
                foreach (var recipient in recipients)
                {
                    var recipientIdData = System.Text.Encoding.UTF8.GetBytes(recipient.Key);
                    cipher.AddKeyRecipient(recipientIdData, recipient.Value);
                }

                var cipherData = cipher.Encrypt(data, true);
                return cipherData;
            }
        }

        /// <summary>
        /// Encrypts text with password.
        /// </summary>
        /// <param name="data">The data to be encrypted</param>
        /// <param name="password">The password</param>
        /// <returns>Encrypted data</returns>
        public static byte[] Encrypt(byte[] data, string password)
        {
            using (var cipher = new VirgilCipher())
            {
                var passwordData = System.Text.Encoding.UTF8.GetBytes(password);
                cipher.AddPasswordRecipient(passwordData);

                var cipherData = cipher.Encrypt(data, true);

                return cipherData;
            }
        }

        /// <summary>
        /// Decrypts encrypted text for recipient with private key.
        /// </summary>
        /// <param name="cipherTextBase64">The encrypted text</param>
        /// <param name="recipientId">The recipient's unique identifier</param>
        /// <param name="privateKey">The private key</param>
        /// <param name="privateKeyPassword">The Private Key's password</param>
        /// <returns>Decrypted text</returns>
        public static string Decrypt(string cipherTextBase64, string recipientId, byte[] privateKey, string privateKeyPassword = null)
        {
            var cipherData = System.Convert.FromBase64String(cipherTextBase64);
            var textData = Decrypt(cipherData, recipientId, privateKey);
            var text = System.Text.Encoding.UTF8.GetString(textData, 0, textData.Length);
            return text;
        }
        
        /// <summary>
        /// Decrypts encrypted text with password. 
        /// </summary>
        /// <param name="cipherTextBase64">Encrypted text in Base64 format</param>
        /// <param name="password">Encrypted text password</param>
        /// <returns>Decrypted text</returns>
        public static string Decrypt(string cipherTextBase64, string password)
        {
            var cipherData = System.Convert.FromBase64String(cipherTextBase64);

            var textData = Decrypt(cipherData, password);
            var text = System.Text.Encoding.UTF8.GetString(textData, 0, textData.Length);

            return text;
        }

        /// <summary>
        /// Decrypts cipher data for recipient ID with private key.
        /// </summary>
        /// <param name="cipherData">Encrypted data</param>
        /// <param name="recipientId">The recipient's unique identifier</param>
        /// <param name="privateKey">The Private Key</param>
        /// <param name="privateKeyPassword">The Private Key's password</param>
        /// <returns>Decrypted data</returns>
        public static byte[] Decrypt(byte[] cipherData, string recipientId, byte[] privateKey, string privateKeyPassword = null)
        {
            using (var cipher = new Virgil.Crypto.VirgilCipher())
            {
                var recipientIdData = System.Text.Encoding.UTF8.GetBytes(recipientId);

                byte[] textData;

                if (privateKeyPassword == null)
                {
                    textData = cipher.DecryptWithKey(cipherData, recipientIdData, privateKey);
                }
                else
                {
                    var privateKeyPasswordData = System.Text.Encoding.UTF8.GetBytes(privateKeyPassword);
                    textData = cipher.DecryptWithKey(cipherData, recipientIdData, privateKey, privateKeyPasswordData);
                }

                return textData;
            }
        }

        /// <summary>
        /// Decrypts encrypted data with password. 
        /// </summary>
        /// <param name="cipherData">Encrypted data</param>
        /// <param name="password">Encrypted text password</param>
        /// <returns>Decrypted data</returns>
        public static byte[] Decrypt(byte[] cipherData, string password)
        {
            using (var cipher = new Virgil.Crypto.VirgilCipher())
            {
                var passwordData = System.Text.Encoding.UTF8.GetBytes(password);
                var textData = cipher.DecryptWithPassword(cipherData, passwordData);
                return textData;
            }
        }

        /// <summary>
        /// Creates digital signature for passing data using private key.
        /// </summary>
        /// <param name="text">Data to be signed.</param>
        /// <param name="privateKey">The Private Key</param>
        /// <param name="privateKeyPassword">The private key password</param>
        /// <returns>The digital signature data</returns>
        public static string Sign(string text, byte[] privateKey, string privateKeyPassword = null)
        {
            var textData = System.Text.Encoding.UTF8.GetBytes(text);

            var signData = Sign(textData, privateKey, privateKeyPassword);
            var signBase64 = System.Convert.ToBase64String(signData);
            return signBase64;
        }

        /// <summary>
        /// Creates digital signature for passing data using private key.
        /// </summary>
        /// <param name="data">Data to be signed.</param>
        /// <param name="privateKey">The Private Key</param>
        /// <param name="privateKeyPassword">The private key password</param>
        /// <returns>The digital signature data</returns>
        public static byte[] Sign(byte[] data, byte[] privateKey, string privateKeyPassword = null)
        {
            using (var signer = new Virgil.Crypto.VirgilSigner())
            {
                var signData = privateKeyPassword == null
                    ? signer.Sign(data, privateKey)
                    : signer.Sign(data, privateKey, System.Text.Encoding.UTF8.GetBytes(privateKeyPassword));

                return signData;
            }
        }

        /// <summary>
        /// Virifies the original text digital signature for owner's public key.
        /// </summary>
        /// <param name="text">The original text</param>
        /// <param name="signBase64">Digital signature in Base64 format</param>
        /// <param name="publicKey">The public key</param>
        /// <returns>True, if digital signtature is valid, otherwise False.</returns>
        public static bool Verify(string text, string signBase64, byte[] publicKey)
        {
            using (var signer = new Virgil.Crypto.VirgilSigner())
            {
                var textData = System.Text.Encoding.UTF8.GetBytes(text);
                var signData = System.Convert.FromBase64String(signBase64);

                var isValid = Verify(textData, signData, publicKey);
                return isValid;
            }
        }

        /// <summary>
        /// Virifies the original data digital signature for owner's public key.
        /// </summary>
        /// <param name="data">The original data</param>
        /// <param name="signData">Digital signature data</param>
        /// <param name="publicKey">The public key</param>
        /// <returns>True, if digitar signtature is valid, otherwise False.</returns>
        public static bool Verify(byte[] data, byte[] signData, byte[] publicKey)
        {
            using (var signer = new Virgil.Crypto.VirgilSigner())
            {
                var isValid = signer.Verify(data, signData, publicKey);
                return isValid;
            }
        }

        public static Virgil.Crypto.VirgilKeyPair GenerateKeyPair(string password = null)
        {
            if (password == null)
            {
                return new VirgilKeyPair();
            }
            
            var passwordData = System.Text.Encoding.UTF8.GetBytes(password);
            return new VirgilKeyPair(passwordData);
        }
    }
}
