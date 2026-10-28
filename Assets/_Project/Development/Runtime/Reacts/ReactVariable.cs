#region

using System;

#endregion

namespace Runtime.Reacts
{
    public class ReactVariable<T> : IReadonlyReactVariable<T> where T : IEquatable<T>
    {
        public event Action<T> ValueChanged;

        public T Value
        {
            get => _value;
            set
            {
                if (_value.Equals(value)) return;

                _value = value;
                ValueChanged?.Invoke(value);
            }
        }

        private T _value;

        public ReactVariable(T initialValue)
        {
            _value = initialValue;
        }

        public ReactVariable()
        {
            _value = default(T);
        }
    }
}