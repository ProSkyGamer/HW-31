#region

using System;
using Runtime.Controllers;
using UnityEngine;

#endregion

namespace Runtime.Movement
{
    public class InputMovementController : Controller, IMovementInput
    {
        public event Action<Vector3> MovementRequested;

        private const string HorizontalInputAxisString = "Horizontal";
        private const string VerticalInputAxisString = "Vertical";
        private readonly float _minInputTreshold = .05f;

        protected override void UpdateInternal(float deltaTime)
        {
            var movementInput = new Vector3(Input.GetAxis(HorizontalInputAxisString), 0f, Input.GetAxis(VerticalInputAxisString));

            if (movementInput.magnitude > _minInputTreshold)
                MovementRequested?.Invoke(movementInput);
        }
    }
}