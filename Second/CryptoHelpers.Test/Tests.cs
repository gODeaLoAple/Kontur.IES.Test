using Moq;

using NUnit.Framework;

using SlowCryptoLib;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoHelpers.Test
{
    public class Tests
    {

        [Test]
        public void Assert_Not_Enumerate_Twice_DifferentKeys()
        {
            var store = new Mock<IStore>();
            var helper = new CryptoHelper(store.Object);

            Assert.Throws<Exception>(() => helper.Sign(Array.Empty<byte>(), "a"));
            Assert.Throws<Exception>(() => helper.Sign(Array.Empty<byte>(), "b"));
            Assert.Throws<Exception>(() => helper.Sign(Array.Empty<byte>(), "c"));

            store.VerifyGet(x => x.Certificates, Times.Once);
        }

        [Test]
        public void Assert_Not_Enumerate_Twice_DuplicateKeys()
        {
            var store = new Mock<IStore>();
            var helper = new CryptoHelper(store.Object);

            Assert.Throws<Exception>(() => helper.Sign(Array.Empty<byte>(), "a"));
            Assert.Throws<Exception>(() => helper.Sign(Array.Empty<byte>(), "a"));
            Assert.Throws<Exception>(() => helper.Sign(Array.Empty<byte>(), "a"));

            store.VerifyGet(x => x.Certificates, Times.Once);
        }


        [TestCase(new string[] { "a", "b" }, new string[] { "a", "a", "b", "c", "d", "" })]
        public void Assert_Not_Enumerate_Twice_DuplicateKeys(string[] firstCertificate, string[] certParams)
        {
            Assert_Not_Enumerate_Twice_DuplicateKeys(new[] { firstCertificate }, certParams);
        }

        [TestCase(new string[] { "a", "b" }, new string[] { "c" }, new string[] { "a", "a", "b", "c", "d", "" })]
        public void Assert_Not_Enumerate_Twice_DuplicateKeys(
            string[] firstCert,
            string[] secondCert,
            string[] certParams)
        {
            Assert_Not_Enumerate_Twice_DuplicateKeys(new[] { firstCert, secondCert }, certParams);
        }

        private void Assert_Not_Enumerate_Twice_DuplicateKeys(string[][] certs, string[] certParams)
        {
            var store = new Mock<IStore>();
            var enumerable = new Mock<IEnumerable<ICertificate>>();
            enumerable.Setup(x => x.GetEnumerator()).Returns(certs.Select(x =>
            {
                var certificate = new Mock<ICertificate>();
                certificate
                    .SetupGet(x => x.CertificateParams)
                    .Returns(x.Select(y => new CertificateParam(y)).ToList())
                    .Verifiable();
                certificate.VerifyGet(x => x.CertificateParams, Times.AtMostOnce);
                return certificate.Object;
            }).GetEnumerator());
            store.SetupGet(x => x.Certificates).Returns(enumerable.Object).Verifiable();
            var helper = new CryptoHelper(store.Object);

            foreach (var param in certParams)
            {
                try
                {
                    helper.Sign(new byte[] { 1, 2, 3 }, param);
                }
                catch { }
            }

            enumerable.Verify(x => x.GetEnumerator(), Times.Once);
            store.VerifyGet(x => x.Certificates, Times.Once);
            Mock.VerifyAll();
        }

    }
}