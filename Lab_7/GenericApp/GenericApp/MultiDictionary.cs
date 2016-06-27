using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericApp
{
    class MultiDictionary<K, V> : IMultiDictionary<K, V>
    {

        private Dictionary<K, LinkedList<V>> multiDictionary;

        public MultiDictionary()
        {
            multiDictionary = new Dictionary<K, LinkedList<V>>();
        } 

        public int Count
        {
            get
            {
                int count = 0;
                foreach (LinkedList<V> keysValues in multiDictionary.Values)
                {
                    count += keysValues.Count;
                }

                return count;
            }
        }

        public ICollection<K> Keys
        {
            get { return multiDictionary.Keys; }
        }

        public ICollection<V> Values
        {
            get
            {
                List<V> values = new List<V>();
                foreach (LinkedList<V> keysValues in multiDictionary.Values)
                {
                    values.AddRange(keysValues.ToList());
                }

                return values;
            }
        }

        public void Add(K key, V value)
        {
            //            if (!multiDictionary.ContainsKey(key))
            //            {
            //                multiDictionary.Add(key, new LinkedList<V>());
            //            }
            //            var valuesList = multiDictionary[key];

            LinkedList<V> valuesList;

            if(multiDictionary.TryGetValue(key, out valuesList))
            {
                valuesList.AddLast(value);
            }
            else
            {
                valuesList = new LinkedList<V>();
                valuesList.AddLast(value);
                multiDictionary.Add(key, valuesList);
            }
        }

        public void Clear()
        {
            multiDictionary.Clear();
        }

        public bool ContainsKey(K key)
        {
            return multiDictionary.ContainsKey(key);
        }

        public bool Contains(K key, V value)
        {
            LinkedList<V> values;
            
            if(multiDictionary.TryGetValue(key, out values))
            {
                return values.Contains(value); 
            }
            return false;
        }

        public bool Remove(K key)
        {
            return multiDictionary.Remove(key);
        }

        public bool Remove(K key, V value)
        {
            LinkedList<V> values;

            if(multiDictionary.TryGetValue(key, out values))
            {
                return values.Remove(value); ;
            }
            return false;

        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            List<KeyValuePair<K, V>> enumerableList = new List<KeyValuePair<K, V>>();
            foreach (var key in multiDictionary.Keys)
            {
                foreach (var value in multiDictionary[key])
                {
                    enumerableList.Add(new KeyValuePair<K, V>(key, value));
                }
                
            }

            return enumerableList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
