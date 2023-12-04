using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NFC.Controller
{
    public class CryptoController
    {
        private byte[] key = new byte[] { 89, 217, 19, 11, 24, 65, 85, 45, 114, 184, 27, 162, 37, 112, 34, 209, 241, 24, 175, 144, 189, 53, 196, 29, 24, 26, 39, 218, 131, 76, 53, 209 };

        // a hardcoded IV should not be used for production AES-CBC code
        // IVs should be unpredictable per ciphertext
        private byte[] iv = new byte[] { 34, 64, 191, 111, 2, 3, 113, 119, 231, 121, 99, 112, 79, 32, 167, 156 };

        private ICryptoTransform encryptor, decryptor;
        private UTF8Encoding encoder;

        public CryptoController()
        {
            AesManaged rm = new AesManaged();
            int s = rm.KeySize;
            rm.Padding = PaddingMode.PKCS7;
            //byte[] iv = new byte[16];
            //new RNGCryptoServiceProvider().GetNonZeroBytes(iv);
            encryptor = rm.CreateEncryptor(key, iv);

            decryptor = rm.CreateDecryptor(key, iv);
            encoder = new UTF8Encoding();
        }

        public string EncryptStringAES(string unencrypted)
        {
            byte[] d = encoder.GetBytes(unencrypted);
            byte[] e = Encrypt(d);
            string res = Convert.ToBase64String(e);
            return res;
        }

        public string DecryptStringAES(string enc)
        {
            string encrypted = enc + "";
            byte[] e = Convert.FromBase64String(encrypted);
            byte[] d = Decrypt(e);
            string res = encoder.GetString(d);
            return res;
        }

        private byte[] Encrypt(byte[] buffer)
        {
            return Transform(buffer, encryptor);
        }

        private byte[] Decrypt(byte[] buffer)
        {
            return Transform(buffer, decryptor);
        }

        protected byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            MemoryStream stream = new MemoryStream();
            using (CryptoStream cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
            {
                cs.Write(buffer, 0, buffer.Length);
            }
            return stream.ToArray();
        }

    }

    public sealed class PasswordHasher
    {
        const int SaltSize = 16, HashSize = 20, HashIter = 10000;
        readonly byte[] _salt, _hash;
        public PasswordHasher(string password)
        {
            new RNGCryptoServiceProvider().GetBytes(_salt = new byte[SaltSize]);
            _hash = new Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize);
        }
        public PasswordHasher(byte[] hashBytes)
        {
            Array.Copy(hashBytes, 0, _salt = new byte[SaltSize], 0, SaltSize);
            Array.Copy(hashBytes, SaltSize, _hash = new byte[HashSize], 0, HashSize);
        }
        public PasswordHasher(byte[] salt, byte[] hash)
        {
            Array.Copy(salt, 0, _salt = new byte[SaltSize], 0, SaltSize);
            Array.Copy(hash, 0, _hash = new byte[HashSize], 0, HashSize);
        }
        public byte[] ToArray()
        {
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(_salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(_hash, 0, hashBytes, SaltSize, HashSize);
            return hashBytes;
        }
        public byte[] Salt { get { return (byte[])_salt.Clone(); } }
        public byte[] Hash { get { return (byte[])_hash.Clone(); } }
        public bool Verify(string password)
        {
            byte[] test = new Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize);
            for (int i = 0; i < HashSize; i++)
                if (test[i] != _hash[i])
                    return false;
            return true;
        }
    }
}