
using System;
using System.Collections;
using System.Collections.Generic;

namespace CryptoHelpers
{
    public class CachedEnumerable<T> : IEnumerable<T>, IDisposable
    {
        private readonly List<T> _cache = new();
        private readonly IEnumerable<T> _collection;
        private IEnumerator<T> _enumerator;

        public CachedEnumerable(IEnumerable<T> collection)
        {
            _enumerator = collection.GetEnumerator();
            _collection = collection;
        }

        public IReadOnlyCollection<T> GetCached() => _cache;

        public void Dispose()
        {
            _cache.Clear();
            _enumerator.Dispose();
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _cache)
            {
                yield return item;
            }

            while (true)
            {
                var hasElement = false;
                try
                {
                    hasElement = _enumerator.MoveNext();
                }
                catch (Exception)
                {
                    hasElement = false;
                    _enumerator = _collection.GetEnumerator();
                    throw;
                }
                if (!hasElement)
                {
                    break;
                }
                if (!_cache.Contains(_enumerator.Current))
                {
                    _cache.Add(_enumerator.Current);
                }
                yield return _enumerator.Current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}