#region

using System;
using UnityEngine;

#endregion

namespace Runtime.Movement
{
    public interface IMovementInput
    {
        public event Action<Vector3> MovementRequested;
    }
}