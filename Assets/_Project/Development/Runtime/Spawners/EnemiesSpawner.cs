#region

using Runtime.Character;
using Runtime.Controllers;
using Runtime.Enemy;
using Runtime.Health;
using Runtime.Movement;
using UnityEngine;

#endregion

namespace Runtime.Spawners
{
    public class EnemiesSpawner
    {
        private readonly ControllerRunnerService _controllerRunnerService;

        public EnemiesSpawner(ControllerRunnerService controllerRunnerService)
        {
            _controllerRunnerService = controllerRunnerService;
        }

        public Enemy.Enemy CreateEnemy(Enemy.Enemy prefab, Vector3 spawnPoint, EnemySettings enemySettings)
        {
            var enemy = Object.Instantiate(prefab, spawnPoint, Quaternion.identity);

            var movementInputController = new RandomMovementController(enemySettings.DirectionChangeInterval);

            IDirectionalMover playerMovement;
            IDirectionalRotator playerRotator;
            if (enemy.TryGetComponent<Rigidbody>(out var rigidbody))
            {
                playerMovement = new RigidbodyDirectionalMover(rigidbody, enemySettings.MoveSpeed);
                playerRotator = new RigidbodyDirectionalRotator(rigidbody, enemySettings.RotationSpeed);
            }
            else
            {
                throw new MissingReferenceException("Player character does not has valuable movement component!");
            }

            var movementController = new MovementController(playerMovement, playerRotator);

            var characterMovementController = new CharacterMovementController(movementController, movementInputController);

            var healthComponent = new HealthComponent(enemySettings.MaxHealth);

            _controllerRunnerService.Register(movementInputController, () => enemy.IsDestroyed);
            movementInputController.Enable();

            _controllerRunnerService.Register(movementController, () => enemy.IsDestroyed);
            movementController.Enable();

            var hitProcessor = new EnemyHitProcessor(enemySettings.DamageOnPlayerHit, enemySettings.HitCooldown);
            _controllerRunnerService.Register(hitProcessor, () => enemy.IsDestroyed);
            hitProcessor.Enable();

            enemy.Initialize(characterMovementController, healthComponent, hitProcessor);

            return enemy;
        }
    }
}