using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BudgetTracker.Services;

public class EncryptionService
{
    private const string AesKey = "WclB+A+rPKHOwW0BZQlpXica0cmj6pRPe+0YTgp5hmE=";
    private const int KeySize = 64;
    private const int Iterations = 350000;
    private readonly HashAlgorithmName m_hashAlgorithm = HashAlgorithmName.SHA512;
    
    public string GeneratePasswordHash(string p_password, out byte[] p_salt)
    {
        p_salt = RandomNumberGenerator.GetBytes(KeySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(p_password),
            p_salt,
            Iterations,
            m_hashAlgorithm,
            KeySize);
        return Convert.ToHexString(hash);
    }

    public bool VerifyPassword(string p_password, string p_hash, byte[] p_salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(p_password, p_salt, Iterations, m_hashAlgorithm, KeySize);
        return hashToCompare.SequenceEqual(Convert.FromHexString(p_hash));
    }
    public string EncryptString(string plainText)
    {
        byte[] iv = new byte[16];
        byte[] array;

        using (Aes aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String("WclB+A+rPKHOwW0BZQlpXica0cmj6pRPe+0YTgp5hmE=");
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(array);
    }

    public string DecryptString(string cipherText)
    {
        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String("WclB+A+rPKHOwW0BZQlpXica0cmj6pRPe+0YTgp5hmE=");
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }
}