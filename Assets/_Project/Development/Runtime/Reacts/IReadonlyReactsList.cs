#region

using System.Collections.Generic;

#endregion

namespace Runtime.Reacts
{
    public interface IReadonlyReactsList<T>
    {
        public IReadOnlyList<T> Value { get; }
        public int Count { get; }
    }
}