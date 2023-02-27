using System.Security.Cryptography;
using System.Text;

namespace Keygen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Programmname: ");
            string productName = Console.ReadLine();

            Console.Write("Name der Webseite: ");
            string clientIdentificationId = Console.ReadLine();

            Console.Write("Aktuelle Versionsnummer: ");
            string versionNumber = Console.ReadLine();

            Console.Write("MAC-Adresse: ");
            string MAC_Adresse = Console.ReadLine();

            Console.WriteLine("");

            string productIdentifier = (productName + "-" + clientIdentificationId + "-" + versionNumber + "-" + MAC_Adresse).ToLower();

            Console.WriteLine(string.Format("Product Identifier: {0}", productIdentifier));
            Console.WriteLine(string.Format("Unformatted License Key: {0}", GetMd5Sum(productIdentifier)));
            Console.WriteLine(string.Format("License Key: {0}", FormatLicenseKey(GetMd5Sum(productIdentifier))));
            Console.ReadLine();
        }

        static string GenerateLicenseKey(string productIdentifier)
        {
            return FormatLicenseKey(GetMd5Sum(productIdentifier));
        }

        static string GetMd5Sum(string productIdentifier)
        {
            Encoder enc = Encoding.Unicode.GetEncoder();
            byte[] unicodeText = new byte[productIdentifier.Length * 2];
            enc.GetBytes(productIdentifier.ToCharArray(), 0, productIdentifier.Length, unicodeText, 0, true);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(unicodeText);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("X2"));
            }
            return sb.ToString();
        }

        static string FormatLicenseKey(string productIdentifier)
        {
            productIdentifier = productIdentifier.Substring(0, 28).ToUpper();
            char[] serialArray = productIdentifier.ToCharArray();
            StringBuilder licenseKey = new StringBuilder();

            int j = 0;
            for (int i = 0; i < 28; i++)
            {
                for (j = i; j < 4 + i; j++)
                {
                    licenseKey.Append(serialArray[j]);
                }
                if (j == 28)
                {
                    break;
                }
                else
                {
                    i = (j) - 1;
                    licenseKey.Append("-");
                }
            }
            return licenseKey.ToString();
        }
    }
}