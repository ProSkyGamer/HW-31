#region

using Runtime.Health;
using Runtime.Reacts;
using Runtime.Utility;
using UnityEngine;

#endregion

namespace Runtime.Character
{
    public class PlayerCharacter : MonoDestroyable, IHaveHealth
    {
        private CharacterMovementController _movementController;
        // на случай если понадобится двигаться внутри контроллера

        private IShootingController _shootingController;
        private IShootingInput _shootingInput;

        private HealthComponent _healthComponent;

        private float _spawnTime;

        [SerializeField] private Transform _bulletShootingPosition;
        public Transform BulletShootingPosition => _bulletShootingPosition;

        public IReadonlyReactVariable<float> CurrentHealth => _healthComponent.Current;
        public IReadonlyReactVariable<float> MaxHealth => _healthComponent.Max;

        public float TimeAlive => Time.time - _spawnTime;

        public void Initialize(CharacterMovementController movementController, IShootingController shootingController,
            IShootingInput shootingInput, HealthComponent healthComponent)
        {
            _movementController = movementController;

            _shootingController = shootingController;
            _shootingInput = shootingInput;

            _healthComponent = healthComponent;

            _spawnTime = Time.time;

            _movementController.Initialize();
            _shootingInput.ShotRequested += ShootingInput_OnShotRequested;

            _healthComponent.Current.ValueChanged += CurrentHealth_OnValueChanged;
        }

        private void CurrentHealth_OnValueChanged(float newHealth)
        {
            if (newHealth <= 0f)
                Destroy();

            Debug.Log($"Player health changed! New health {newHealth}");
        }

        private void ShootingInput_OnShotRequested()
        {
            _shootingController.Shoot();
        }

        public void TakeDamage(float amount)
        {
            Debug.Log($"taking damage {amount}");

            _healthComponent.TakeDamage(amount);
        }

        public void Heal(float amount)
        {
            _healthComponent.Heal(amount);
        }
    }
}