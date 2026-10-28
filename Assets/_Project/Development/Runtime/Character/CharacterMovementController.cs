#region

using Runtime.Movement;
using UnityEngine;

#endregion

namespace Runtime.Character
{
    public class CharacterMovementController
    {
        public IMovementController MovementController { get; private set; }
        public IMovementInput MovementInput { get; private set; }

        public CharacterMovementController(IMovementController movementController, IMovementInput movementInput)
        {
            MovementController = movementController;
            MovementInput = movementInput;
        }

        public void Initialize()
        {
            MovementInput.MovementRequested += MovementInput_OnMovementRequested;
        }

        private void MovementInput_OnMovementRequested(Vector3 direction)
        {
            MovementController.MoveDirection(direction);
        }
    }
}