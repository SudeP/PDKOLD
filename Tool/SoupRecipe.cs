using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PDK.Tool
{
    public class SoupRecipe
    {
        private Const @const;
        private Tool tool;
        private Numbers numbers;
        public string Stir(string pureSoup, string salt)
        {
            ParameterIsNull(pureSoup);
            ParameterIsNull(salt);
            Initialize();

            string impureSoup = string.Empty;
            try
            {
                impureSoup = numbers.ToString() +
                ModedListToCrypteString(
                    QuantumListToModedList(
                        BinaryListToQunatumList(
                            StringToBinaryList(
                                Cipher.Encrypt(pureSoup, salt)
                            )
                        )
                    )
                );
            }
            catch { }
            return impureSoup;
        }
        public string Fractionate(string impureSoup, string salt)
        {
            ParameterIsNull(impureSoup);
            ParameterIsNull(salt);
            Initialize(impureSoup[0]);

            impureSoup = impureSoup.Substring(1);
            string pureSoup = string.Empty;
            try
            {
                pureSoup =
                Cipher.Decrypt(
                    BinaryListToDecrypteString(
                        QuantumListToBinaryList(
                            ModedListToQuantumList(
                                CrypteStringToModedList(impureSoup)
                            )
                        )
                    )
                , salt);
            }
            catch { }
            return pureSoup;
        }
        private List<string> StringToBinaryList(string @string)
        {
            List<string> vs = new List<string>();
            foreach (char c in @string)
                vs.Add(Convert.ToString(c, @const.binarySystemCode).PadLeft(@const.padCount, @const.padChar));
            return vs;
        }
        private List<string> BinaryListToQunatumList(List<string> binaryList)
        {
            List<string> vs = new List<string>();

            for (int i = 0; i < binaryList.Count; i++)
            {
                var v = binaryList[i];
                var newNumber = string.Empty;
                for (int iSUB = 0; iSUB < v.Length; iSUB += 2)
                {
                    for (int iSUBsuperSUB = 0; iSUBsuperSUB < numbers.Quantum.Length; iSUBsuperSUB++)
                    {
                        var vSUB = v[iSUB] + (v[iSUB + 1] + "");
                        if (numbers.Quantum[iSUBsuperSUB, 0] == vSUB)
                        {
                            newNumber += numbers.Quantum[iSUBsuperSUB, 1];
                            break;
                        }
                    }
                }
                vs.Add(newNumber);
            }

            return vs;
        }
        private List<string> QuantumListToModedList(List<string> quantumList)
        {
            List<string> vs = new List<string>();

            for (int i = 0; i < quantumList.Count; i++)
            {
                var v = quantumList[i];
                var divide = long.Parse(v) / (long)char.MaxValue;
                divide = int.Parse(divide.ToString().PadRight(@const.quantumPadCount, @const.padChar));
                var mod = long.Parse(v) % (long)char.MaxValue;
                vs.Add((char)divide + ((char)mod + ""));
            }
            return vs;
        }
        private string ModedListToCrypteString(List<string> modedList)
        {
            string rev = string.Empty;

            for (int i = 0; i < modedList.Count; i++)
                rev += modedList[i];

            return rev;
        }
        private List<string> CrypteStringToModedList(string crypteString)
        {
            List<string> vs = new List<string>();

            for (int i = 0; i < crypteString.Length; i += 2)
                vs.Add(crypteString[i] + (crypteString[i + 1] + ""));

            return vs;
        }
        private List<string> ModedListToQuantumList(List<string> modedList)
        {
            List<string> vs = new List<string>();

            for (int i = 0; i < modedList.Count; i++)
            {
                var v = modedList[i];

                var divide = (long)v[0];
                if (divide > 1525)
                    divide = long.Parse(divide.ToString().Substring(0, 2));

                divide *= (long)char.MaxValue;

                var mod = (long)v[1];

                vs.Add((divide + mod).ToString());
            }

            return vs;
        }
        private List<string> QuantumListToBinaryList(List<string> quantumList)
        {
            List<string> vs = new List<string>();

            for (int i = 0; i < quantumList.Count; i++)
            {
                var v = quantumList[i];
                var newNumber = string.Empty;

                for (int iSUB = 0; iSUB < v.Length; iSUB++)
                {
                    for (int iSUBsuperSUB = 0; iSUBsuperSUB < numbers.Quantum.GetLongLength(0); iSUBsuperSUB++)
                    {
                        var vSUB = v[iSUB] + "";
                        if (numbers.Quantum[iSUBsuperSUB, 1] == vSUB)
                        {
                            newNumber += numbers.Quantum[iSUBsuperSUB, 0];
                            break;
                        }
                    }
                }
                vs.Add(newNumber);
            }

            return vs;
        }
        private string BinaryListToDecrypteString(List<string> binaryList)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < binaryList.Count; i++)
                byteList.Add(Convert.ToByte(binaryList[i], @const.binarySystemCode));

            string rev = Encoding.ASCII.GetString(byteList.ToArray());

            return rev;
        }
        private void Initialize(char c = '\0')
        {
            @const = new Const();
            tool = new Tool();
            numbers = new Numbers(tool, c);
        }
        private void ParameterIsNull(string param)
        {
            if (param is null)
                throw new ArgumentNullException();
            else if (param is "")
                throw new ArgumentOutOfRangeException("string.Legth > 0 required");
        }
    }
    internal class Const
    {
        public readonly int binarySystemCode = 2;
        public readonly int quantumPadCount = 3;
        public readonly int padCount = 16;
        public readonly char padChar = '0';
    }
    internal class Tool
    {
        public readonly Random random = new Random();
    }
    internal class Numbers
    {
        private readonly string[] ___numbers = new string[]
        {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"
        };
        public string[,] Quantum { get; private set; }
        private readonly Tool tool;
        public Numbers(Tool tool, char c = '\0')
        {
            this.tool = tool;
            if (c != '\0')
                SetQuantumValues(((int)c).ToString().Select(@char => @char.ToString()).ToArray());
            else
                SetQuantumValues(GenerateNumbers());

        }
        private void SetQuantumValues(string[] numbers)
        {
            Quantum = new string[,]
            {
                { "00" , numbers.Length == 4 ? numbers[0] : GetNumber(ref numbers) },
                { "01" , numbers.Length == 4 ? numbers[1] : GetNumber(ref numbers) },
                { "10" , numbers.Length == 4 ? numbers[2] : GetNumber(ref numbers) },
                { "11" , numbers.Length == 4 ? numbers[3] : GetNumber(ref numbers) }
            };
        }
        private string[] GenerateNumbers()
        {
            var tempNumbers = ___numbers.ToArray();
            string[] numbers = new string[tempNumbers.Length];

            for (int i = 0; i < numbers.Length; i++)
                numbers[i] = GetNumber(ref tempNumbers);

            return numbers;
        }
        string GetNumber(ref string[] numbers)
        {
            var i = tool.random.Next(0, numbers.Length);
            var v = numbers[i];

            var temp = numbers.ToList();
            temp.RemoveAt(i);
            numbers = temp.ToArray();

            return v;
        }
        public override string ToString()
        {
            string rev = string.Empty;
            for (int i = 0; i < Quantum.GetLongLength(0); i++)
                rev += Quantum[i, 1];
            return (char)long.Parse(rev) + "";
        }
    }
    public class Base64
    {
        public static string Encrypt(string plainText)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
        }
        public static string Decrypt(string encryptedText)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encryptedText));
        }
    }
    public class Cipher
    {
        public static string Encrypt(string plainText, string password)
        {
            if (plainText == null)
                return null;

            if (password == null)
                password = string.Empty;

            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            using (SHA256 sHA256 = SHA256.Create())
                passwordBytes = sHA256.ComputeHash(passwordBytes);

            var bytesEncrypted = Encrypt(bytesToBeEncrypted, passwordBytes);

            return Convert.ToBase64String(bytesEncrypted);
        }
        public static string Decrypt(string encryptedText, string password)
        {
            if (encryptedText == null)
                return null;

            if (password == null)
                password = string.Empty;

            var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            using (SHA256 sHA256 = SHA256.Create())
                passwordBytes = sHA256.ComputeHash(passwordBytes);

            var bytesDecrypted = Decrypt(bytesToBeDecrypted, passwordBytes);

            return Encoding.UTF8.GetString(bytesDecrypted);
        }
        private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using RijndaelManaged AES = new RijndaelManaged();
                using Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                AES.KeySize = 256;

                AES.BlockSize = 128;
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);

                AES.Mode = CipherMode.CBC;

                using (CryptoStream cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                    cs.Close();
                }

                encryptedBytes = ms.ToArray();
            }

            return encryptedBytes;
        }
        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using RijndaelManaged AES = new RijndaelManaged();
                using Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                AES.KeySize = 256;

                AES.BlockSize = 128;
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);
                AES.Mode = CipherMode.CBC;

                using (CryptoStream cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                    cs.Close();
                }

                decryptedBytes = ms.ToArray();
            }

            return decryptedBytes;
        }
    }
}
