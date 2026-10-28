#region

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

        public PlayerCharacter CreatePlayerCharacterFrom(PlayerCharacterSettings playerCharacterSettings)
        {
            var playerCharacter = Object.Instantiate(playerCharacterSettings.Prefab, playerCharacterSettings.SpawnPoint, Quaternion.identity, null);
            var movementInputController = new InputMovementController();

            IDirectionalMover playerMovement;
            IDirectionalRotator playerRotator;
            if (playerCharacter.TryGetComponent<Rigidbody>(out var rigidbody))
            {
                playerMovement = new RigidbodyDirectionalMover(rigidbody, playerCharacterSettings.MoveSpeed);
                playerRotator = new RigidbodyDirectionalRotator(rigidbody, playerCharacterSettings.RotationSpeed);
            }
            else
            {
                throw new MissingReferenceException("Player character does not has valuable movement component!");
            }

            playerRotator.Rotate(playerCharacter.transform.forward);

            var movementController = new MovementController(playerMovement, playerRotator);

            var characterMovementController = new CharacterMovementController(movementController, movementInputController);

            var shootingInputController = new CharacterShootingInputController();
            var playerShootingController = new CharacterFiring(playerCharacterSettings.BulletPrefab, playerCharacter.BulletShootingPosition,
                playerCharacterSettings.BulletDamage, playerCharacterSettings.BulletSpeed, playerRotator);

            var healthComponent = new HealthComponent(playerCharacterSettings.MaxHealth);

            _controllerRunnerService.Register(movementInputController, () => playerCharacter.IsDestroyed);
            _controllerRunnerService.Register(shootingInputController, () => playerCharacter.IsDestroyed);
            movementInputController.Enable();
            shootingInputController.Enable();

            _controllerRunnerService.Register(movementController, () => playerCharacter.IsDestroyed);
            movementController.Enable();


            playerCharacter.Initialize(characterMovementController, playerShootingController, shootingInputController, healthComponent);

            return playerCharacter;
        }
    }
}