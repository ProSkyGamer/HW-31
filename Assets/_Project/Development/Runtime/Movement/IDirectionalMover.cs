#region

using UnityEngine;

#endregion

namespace Runtime.Movement
{
    public interface IDirectionalMover : IRotationDirection
    {
        public void Move(Vector3 direction);
    }
}