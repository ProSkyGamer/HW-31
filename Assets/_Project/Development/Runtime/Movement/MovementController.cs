#region

using Runtime.Controllers;
using UnityEngine;

#endregion

namespace Runtime.Movement
{
    public class MovementController : Controller, IMovementController
    {
        private readonly IDirectionalMover _directionalMover;
        private readonly IDirectionalRotator _directionalRotator;

        private bool _isMovementRequested;
        private Vector3 _direction;

        public MovementController(IDirectionalMover directionalMover, IDirectionalRotator directionalRotator)
        {
            _directionalMover = directionalMover;
            _directionalRotator = directionalRotator;
        }

        public void MoveDirection(Vector3 direction)
        {
            if (IsActive == false)
                return;

            _isMovementRequested = true;
            _direction = direction;
        }

        protected override void UpdateInternal(float deltaTime)
        {
            if (_isMovementRequested)
            {
                _directionalMover.Move(_direction);
                _directionalRotator.Rotate(_direction);

                _isMovementRequested = false;
            }
        }
    }
}