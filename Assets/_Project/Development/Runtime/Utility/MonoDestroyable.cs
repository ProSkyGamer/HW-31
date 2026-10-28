#region

using System;
using UnityEngine;

#endregion

namespace Runtime.Utility
{
    public class MonoDestroyable : MonoBehaviour
    {
        public event Action<MonoDestroyable> Destroyed;

        public bool IsDestroyed { get; private set; }

        public void Destroy()
        {
            IsDestroyed = true;

            Destroyed?.Invoke(this);

            Destroy(gameObject);
        }
    }
}