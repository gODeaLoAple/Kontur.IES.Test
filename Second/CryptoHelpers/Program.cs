using System;
using System.Linq;
using System.Text;

using SlowCryptoLib;

namespace CryptoHelpers
{
    public static class Program
    {
        private const string CertParam = "cert_param_0";
        private static readonly byte[] DataToSign = Encoding.Unicode.GetBytes("Строка для подписания");

        public static void Main()
        {
            var store = new Store();

            var certificate = store.Certificates.First();
            var firstParam = certificate.CertificateParams.First();
            Console.WriteLine($"Первый сертификат содержит параметр '{CertParam}' ({firstParam.Is(CertParam)}).");

            var signature = certificate.Sign(DataToSign);
            Console.Write($"Подпись: '{Convert.ToBase64String(signature)[..10]}...' ");
            Console.WriteLine($"проходит верификацию ({certificate.Verify(signature)}).");

            Console.WriteLine("Все извлеченные сертификаты должны быть закрыты перед закрытием хранилища.");
            certificate.Dispose();
            store.Dispose();
        }
    }
}