using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dictionary
{
    [Serializable]
    public class Dictionary<K, V>
    {
        [SerializeField] private List<DictionaryElement> _dictionary;

        public int Count => _dictionary.Count;

        public void Add(K key, V value)
        {
            _dictionary.Add(new DictionaryElement(key, value));
        }

        public bool TryGet(K key, out V value)
        {
            foreach (DictionaryElement item in _dictionary)
            {
                if (item.Key.Equals(key))
                {
                    value = item.Value;
                    return true;
                }
            }

            value = default;
            return false;
        }

        public K GetKey(int index)
        {
            return _dictionary[index].Key;
        }

        public V GetValue(int index)
        {
            return _dictionary[index].Value;
        }

        [Serializable]
        private class DictionaryElement
        {
            public DictionaryElement(K key, V value)
            {
                Key = key;
                Value = value;
            }

            [field: SerializeField] public K Key { get; private set; }
            [field: SerializeField] public V Value { get; private set; }
        }
    }
}
