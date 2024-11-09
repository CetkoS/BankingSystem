using System.Collections.Concurrent;

namespace BankingSystem.Models
{
    public class ConcurrentSet<T>
        where T : class
    {
        private readonly ConcurrentDictionary<T, byte> _dictionary;

        public ConcurrentSet()
        {
            _dictionary = new ConcurrentDictionary<T, byte>();
        }

        public bool Contains(T item) => _dictionary.ContainsKey(item);

        public bool Add(T item) => _dictionary.TryAdd(item, 0);
    }
}
