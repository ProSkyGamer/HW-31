#region

using UnityEngine;

#endregion

namespace Runtime.Movement
{
    public interface IMovementController
    {
        public void MoveDirection(Vector3 direction);
    }
}