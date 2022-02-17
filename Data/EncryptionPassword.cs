using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SecurePass.Data
{
    /// <summary>
    ///     basic Encrption/decryption functionality
    /// </summary>
    public class EncryptionPassword
    {
        #region enums, constants & fields

        //types of symmetric encyption
        public enum CryptoTypes
        {
            EncTypeDes = 0,
            EncTypeRc2,
            EncTypeRijndael,
            EncTypeTripleDes
        }

        private const string CryptDefaultPassword = "CB06cfE507a1";
        private const CryptoTypes CryptDefaultMethod = CryptoTypes.EncTypeRijndael;

        private byte[] _mKey = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24};
        private byte[] _mIv = {65, 110, 68, 26, 69, 178, 200, 219};

        private readonly byte[] _saltByteArray =
            {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76};

        private CryptoTypes _mCryptoType = CryptDefaultMethod;
        private string _mPassword = CryptDefaultPassword;

        #endregion

        #region Constructors

        public EncryptionPassword()
        {
            CalculateNewKeyAndIv();
        }

        public EncryptionPassword(CryptoTypes cryptoType)
        {
            CryptoType = cryptoType;
        }

        #endregion

        #region Props

        /// <summary>
        ///     type of encryption / decryption used
        /// </summary>
        public CryptoTypes CryptoType
        {
            get => _mCryptoType;
            set
            {
                if (_mCryptoType != value)
                {
                    _mCryptoType = value;
                    CalculateNewKeyAndIv();
                }
            }
        }

        /// <summary>
        ///     Passsword Key Property.
        ///     The password key used when encrypting / decrypting
        /// </summary>
        public string Password
        {
            get => _mPassword;
            set
            {
                if (_mPassword != value)
                {
                    _mPassword = value;
                    CalculateNewKeyAndIv();
                }
            }
        }

        #endregion

        #region Encryption

        /// <summary>
        ///     Encrypt a string
        /// </summary>
        /// <param name="inputText">text to encrypt</param>
        /// <returns>an encrypted string</returns>
        public string Encrypt(string inputText)
        {
            //declare a new encoder
            var utf8Encoder = new UTF8Encoding();
            //get byte representation of string
            var inputBytes = utf8Encoder.GetBytes(inputText);

            //convert back to a string
            return Convert.ToBase64String(EncryptDecrypt(inputBytes, true));
        }

        /// <summary>
        ///     Encrypt string with user defined password
        /// </summary>
        /// <param name="inputText">text to encrypt</param>
        /// <param name="password">password to use when encrypting</param>
        /// <returns>an encrypted string</returns>
        public string Encrypt(string inputText, string password)
        {
            Password = password;
            return Encrypt(inputText);
        }

        /// <summary>
        ///     Encrypt string acc. to cryptoType and with user defined password
        /// </summary>
        /// <param name="inputText">text to encrypt</param>
        /// <param name="password">password to use when encrypting</param>
        /// <param name="cryptoType">type of encryption</param>
        /// <returns>an encrypted string</returns>
        public string Encrypt(string inputText, string password, CryptoTypes cryptoType)
        {
            _mCryptoType = cryptoType;
            return Encrypt(inputText, password);
        }

        /// <summary>
        ///     Encrypt string acc. to cryptoType
        /// </summary>
        /// <param name="inputText">text to encrypt</param>
        /// <param name="cryptoType">type of encryption</param>
        /// <returns>an encrypted string</returns>
        public string Encrypt(string inputText, CryptoTypes cryptoType)
        {
            CryptoType = cryptoType;
            return Encrypt(inputText);
        }

        #endregion

        #region Decryption

        /// <summary>
        ///     decrypts a string
        /// </summary>
        /// <param name="inputText">string to decrypt</param>
        /// <returns>a decrypted string</returns>
        public string Decrypt(string inputText)
        {
            //declare a new encoder
            var utf8Encoder = new UTF8Encoding();
            //get byte representation of string
            var inputBytes = Convert.FromBase64String(inputText);

            //convert back to a string
            return utf8Encoder.GetString(EncryptDecrypt(inputBytes, false));
        }

        /// <summary>
        ///     decrypts a string using a user defined password key
        /// </summary>
        /// <param name="inputText">string to decrypt</param>
        /// <param name="password">password to use when decrypting</param>
        /// <returns>a decrypted string</returns>
        public string Decrypt(string inputText, string password)
        {
            Password = password;
            return Decrypt(inputText);
        }

        /// <summary>
        ///     decrypts a string acc. to decryption type and user defined password key
        /// </summary>
        /// <param name="inputText">string to decrypt</param>
        /// <param name="password">password key used to decrypt</param>
        /// <param name="cryptoType">type of decryption</param>
        /// <returns>a decrypted string</returns>
        public string Decrypt(string inputText, string password, CryptoTypes cryptoType)
        {
            _mCryptoType = cryptoType;
            return Decrypt(inputText, password);
        }

        /// <summary>
        ///     decrypts a string acc. to the decryption type
        /// </summary>
        /// <param name="inputText">string to decrypt</param>
        /// <param name="cryptoType">type of decryption</param>
        /// <returns>a decrypted string</returns>
        public string Decrypt(string inputText, CryptoTypes cryptoType)
        {
            CryptoType = cryptoType;
            return Decrypt(inputText);
        }

        #endregion

        #region Symmetric Engine

        /// <summary>
        ///     performs the actual enc/dec.
        /// </summary>
        /// <param name="inputBytes">input byte array</param>
        /// <param name="encrpyt">wheather or not to perform enc/dec</param>
        /// <returns>byte array output</returns>
        private byte[] EncryptDecrypt(byte[] inputBytes, bool encrpyt)
        {
            //get the correct transform
            var transform = GetCryptoTransform(encrpyt);

            //memory stream for output
            var memStream = new MemoryStream();

            try
            {
                //setup the cryption - output written to memstream
                var cryptStream = new CryptoStream(memStream, transform, CryptoStreamMode.Write);

                //write data to cryption engine
                cryptStream.Write(inputBytes, 0, inputBytes.Length);

                //we are finished
                cryptStream.FlushFinalBlock();

                //get result
                var output = memStream.ToArray();

                //finished with engine, so close the stream
                cryptStream.Close();

                return output;
            }
            catch (Exception e)
            {
                //throw an error
                throw new Exception("Error in symmetric engine. Error : " + e.Message, e);
            }
        }

        /// <summary>
        ///     returns the symmetric engine and creates the encyptor/decryptor
        /// </summary>
        /// <param name="encrypt">whether to return a encrpytor or decryptor</param>
        /// <returns>ICryptoTransform</returns>
        private ICryptoTransform GetCryptoTransform(bool encrypt)
        {
            var sa = SelectAlgorithm();
            sa.Key = _mKey;
            sa.IV = _mIv;
            if (encrypt)
                return sa.CreateEncryptor();
            return sa.CreateDecryptor();
        }

        /// <summary>
        ///     returns the specific symmetric algorithm acc. to the cryptotype
        /// </summary>
        /// <returns>SymmetricAlgorithm</returns>
        private SymmetricAlgorithm SelectAlgorithm()
        {
            SymmetricAlgorithm sa;
            switch (_mCryptoType)
            {
                case CryptoTypes.EncTypeDes:
                    sa = DES.Create();
                    break;
                case CryptoTypes.EncTypeRc2:
                    sa = RC2.Create();
                    break;
                case CryptoTypes.EncTypeRijndael:
                    sa = Rijndael.Create();
                    break;
                case CryptoTypes.EncTypeTripleDes:
                    sa = TripleDES.Create();
                    break;
                default:
                    sa = TripleDES.Create();
                    break;
            }

            return sa;
        }

        /// <summary>
        ///     calculates the key and IV acc. to the symmetric method from the password
        ///     key and IV size dependant on symmetric method
        /// </summary>
        private void CalculateNewKeyAndIv()
        {
            //use salt so that key cannot be found with dictionary attack
            var pdb = new PasswordDeriveBytes(_mPassword, _saltByteArray);
            var algo = SelectAlgorithm();
            _mKey = pdb.GetBytes(algo.KeySize / 8);
            _mIv = pdb.GetBytes(algo.BlockSize / 8);
        }

        #endregion
    }
}