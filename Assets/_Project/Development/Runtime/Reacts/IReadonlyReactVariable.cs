#region

using System;

#endregion

namespace Runtime.Reacts
{
    public interface IReadonlyReactVariable<T> where T : IEquatable<T>
    {
        public event Action<T> ValueChanged;

        public T Value { get; }
    }
}