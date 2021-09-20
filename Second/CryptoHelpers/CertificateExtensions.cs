using SlowCryptoLib;

using System.Linq;

namespace CryptoHelpers
{
    public static class CertificateExtensions
    {
        public static bool HasParam(this ICertificate certificate, string certParamValue) 
            => certificate != null && certificate.CertificateParams.Any(x => x.Is(certParamValue));
    }
}