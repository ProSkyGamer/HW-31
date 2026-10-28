#region

using System;
using System.Collections.Generic;

#endregion

namespace Runtime.Reacts
{
    public class ReactsList<T> : IReadonlyReactsList<T> where T : IEquatable<T>
    {
        public event Action ListUpdated;

        private readonly List<T> _value;

        public IReadOnlyList<T> Value => _value;

        public int Count => _value.Count;

        public ReactsList(IEnumerable<T> defaultValue)
        {
            _value = new(defaultValue);
        }

        public ReactsList()
        {
            _value = new();
        }

        public void Add(T element)
        {
            if (element == null)
                return;

            _value.Add(element);

            ListUpdated?.Invoke();
        }

        public void AddRange(IEnumerable<T> elements)
        {
            if (elements == null)
                return;

            _value.AddRange(elements);

            ListUpdated?.Invoke();
        }

        public void Remove(T element)
        {
            if (_value.Contains(element))
            {
                _value.Remove(element);

                ListUpdated?.Invoke();
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                return;

            _value.RemoveAt(index);

            ListUpdated?.Invoke();
        }

        public void Clear()
        {
            if (Count <= 0)
                return;

            _value.Clear();

            ListUpdated?.Invoke();
        }
    }
}