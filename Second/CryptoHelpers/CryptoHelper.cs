using SlowCryptoLib;

using System;
using System.Linq;

namespace CryptoHelpers
{
    public class CryptoHelper : ICryptoHelper
    {
        private readonly IStore _store;
        private readonly CachedEnumerable<FastParamSearchWrapper> _cache;

        public CryptoHelper(IStore store)
        {
            _store = store;
            var wrappers = _store.Certificates.Select(x => new FastParamSearchWrapper(x));
            _cache = new CachedEnumerable<FastParamSearchWrapper>(wrappers);
        }

        public byte[] Sign(byte[] data, string certParamValue)
        {
            return FindCertificate(certParamValue).Sign(data);
        }

        public bool Verify(byte[] signature, string certParamValue)
        {
            return FindCertificate(certParamValue).Verify(signature);
        }

        public void Dispose()
        {
            foreach (var cert in _cache.GetCached())
            {
                cert.Dispose();
            }
            _cache.Dispose();
            _store.Dispose();
        }

        private ICertificate FindCertificate(string certParamValue)
        {
            var certificate = _cache.FirstOrDefault(x => x.HasParam(certParamValue));
            if (certificate is not null)
            {
                return certificate.Certificate;
            }
            return FindCertificateInStoreByParamAndCacheOtherOnWay(certParamValue);
        }

        private ICertificate FindCertificateInStoreByParamAndCacheOtherOnWay(string certParamValue)
        {
            foreach (var cert in _cache)
            {
                if (cert.HasParam(certParamValue))
                {
                    return cert.Certificate;
                }
            }
            throw new Exception();
        }

    }
}