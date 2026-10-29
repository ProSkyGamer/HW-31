#region

using System;
using Runtime.Character;
using Runtime.Health;
using Runtime.Reacts;
using Runtime.Utility;
using UnityEngine;

#endregion

namespace Runtime.Enemy
{
    public class Enemy : MonoDestroyable, IHaveHealth, IEquatable<Enemy>
    {
        private CharacterMovementController _movementController;

        private HealthComponent _healthComponent;
        private IHitProcessor _hitProcessor;

        public IReadonlyReactVariable<float> CurrentHealth => _healthComponent.Current;
        public IReadonlyReactVariable<float> MaxHealth => _healthComponent.Max;

        public void Initialize(CharacterMovementController movementController, HealthComponent healthComponent, IHitProcessor hitProcessor)
        {
            _movementController = movementController;

            _healthComponent = healthComponent;
            _hitProcessor = hitProcessor;

            _movementController.Initialize();

            healthComponent.Current.ValueChanged += CurrentHealth_OnValueChanged;
        }

        private void CurrentHealth_OnValueChanged(float newHealth)
        {
            if (newHealth <= 0f)
                Destroy();

            Debug.Log($"Enemy {gameObject.name} took damage! New health {newHealth}");
        }

        public void OnCollisionEnter(Collision other)
        {
            _hitProcessor.ProcessHit(other);
        }

        public void TakeDamage(float amount)
        {
            _healthComponent.TakeDamage(amount);
        }

        public void Heal(float amount)
        {
            _healthComponent.Heal(amount);
        }

        public bool Equals(Enemy other)
        {
            if (other == null)
                return false;

            return other.gameObject.GetInstanceID() == gameObject.GetInstanceID();
        }
    }
}