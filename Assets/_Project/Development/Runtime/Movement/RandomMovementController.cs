#region

using System;
using Runtime.Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

namespace Runtime.Movement
{
    public class RandomMovementController : Controller, IMovementInput
    {
        public event Action<Vector3> MovementRequested;

        private readonly float _directionSwapInterval;
        private float _tillDirectionSwap;

        private Vector3 _currentDirection;

        public RandomMovementController(float directionSpawnInterval)
        {
            _directionSwapInterval = directionSpawnInterval;

            _currentDirection = GetRandomNormalizedDirection();
        }

        protected override void UpdateInternal(float deltaTime)
        {
            ProcessDirectionChange(deltaTime);
            ProcessDirectionInput();
        }

        private void ProcessDirectionChange(float deltaTime)
        {
            _tillDirectionSwap -= deltaTime;

            if (_tillDirectionSwap <= 0)
            {
                _currentDirection = GetRandomNormalizedDirection();
                _tillDirectionSwap = _directionSwapInterval;
            }
        }

        private void ProcessDirectionInput()
        {
            MovementRequested?.Invoke(_currentDirection);
        }

        private Vector3 GetRandomNormalizedDirection()
        {
            var xDirection = Random.Range(-1f, 1f);
            var zDirection = Random.Range(-1f, 1f);

            var randomDirection = new Vector3(xDirection, 0f, zDirection);

            return randomDirection.normalized;
        }
    }
}