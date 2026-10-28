#region

using UnityEngine;

#endregion

namespace Runtime.Movement
{
    public interface IDirectionalRotator : IRotationDirection
    {
        public void Rotate(Vector3 direction);
    }
}