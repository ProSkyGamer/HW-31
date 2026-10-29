#region

using Cinemachine;
using Runtime.Character;
using Runtime.Controllers;
using Runtime.Health;
using Runtime.Movement;
using UnityEngine;

#endregion

namespace Runtime.Spawners
{
    public class PlayerCharacterSpawner
    {
        private readonly ControllerRunnerService _controllerRunnerService;

        public PlayerCharacterSpawner(ControllerRunnerService controllerRunnerService)
        {
            _controllerRunnerService = controllerRunnerService;
        }

        public PlayerCharacter CreatePlayerCharacterFrom(PlayerCharacterSettings playerCharacterSettings, CinemachineVirtualCamera followCamera)
        {
            PlayerCharacter playerCharacter =
                Object.Instantiate(playerCharacterSettings.Prefab, playerCharacterSettings.SpawnPoint, Quaternion.identity, null);

            InputMovementController movementInputController = new();

            IDirectionalMover playerMovement;
            IDirectionalRotator playerRotator;

            if (playerCharacter.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                playerMovement = new RigidbodyDirectionalMover(rigidbody, playerCharacterSettings.MoveSpeed);
                playerRotator = new RigidbodyDirectionalRotator(rigidbody, playerCharacterSettings.RotationSpeed);
            }
            else
            {
                throw new MissingReferenceException("Player character does not has valuable movement component!");
            }

            playerRotator.Rotate(playerCharacter.transform.forward);

            MovementController movementController = new(playerMovement, playerRotator);

            CharacterMovementController characterMovementController = new(movementController, movementInputController);

            CharacterShootingInputController shootingInputController = new();

            CharacterFiring playerShootingController = new(playerCharacterSettings.BulletPrefab, playerCharacter.BulletShootingPosition,
                playerCharacterSettings.BulletDamage, playerCharacterSettings.BulletSpeed, playerRotator);

            HealthComponent healthComponent = new(playerCharacterSettings.MaxHealth);

            _controllerRunnerService.Register(movementInputController, () => playerCharacter.IsDestroyed);
            _controllerRunnerService.Register(shootingInputController, () => playerCharacter.IsDestroyed);
            movementInputController.Enable();
            shootingInputController.Enable();

            _controllerRunnerService.Register(movementController, () => playerCharacter.IsDestroyed);
            movementController.Enable();

            playerCharacter.Initialize(characterMovementController, playerShootingController, shootingInputController, healthComponent);

            followCamera.Follow = playerCharacter.LookAtTarget;
            followCamera.LookAt = playerCharacter.LookAtTarget;

            return playerCharacter;
        }
    }
}