using SlowCryptoLib;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoHelpers
{
    public class FastParamSearchWrapper : IDisposable
    {
        public ICertificate Certificate { get; }

        private readonly HashSet<string> _params = new();


        private readonly CachedEnumerable<ICertificateParam> _cachedParams;
        public FastParamSearchWrapper(ICertificate certificate)
        {
            Certificate = certificate;
            _cachedParams = new CachedEnumerable<ICertificateParam>(certificate.CertificateParams);
        }

        public void Dispose()
        {
            Certificate.Dispose();
        }

        public bool HasParam(string param)
        {
            if (_params.Contains(param) || _cachedParams.Any(x => x.Is(param)))
            {
                _params.Add(param);
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            return obj is FastParamSearchWrapper wrapper && wrapper.Certificate == Certificate;
        }

        public override int GetHashCode()
        {
            return Certificate.GetHashCode();
        }
    }
}