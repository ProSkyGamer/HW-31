#region

using System;
using Runtime.Reacts;
using UnityEngine;

#endregion

namespace Runtime.Health
{
    public class HealthComponent
    {
        private readonly ReactVariable<float> _current;
        private readonly ReactVariable<float> _max;

        public IReadonlyReactVariable<float> Current => _current;
        public IReadonlyReactVariable<float> Max => _max;

        public HealthComponent(float maxHealth)
        {
            if (maxHealth <= 0)
                throw new ArgumentOutOfRangeException($"Max health less or equalth the zero! {maxHealth}");

            _max = new(maxHealth);
            _current = new(_max.Value);
        }

        public void TakeDamage(float amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException($"Trying to deal damage less then zero ({amount})!");

            _current.Value = Mathf.Clamp(_current.Value - amount, 0f, _max.Value);
        }

        public void Heal(float amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException($"Trying to heal less then zero ({amount})!");

            _current.Value = Mathf.Clamp(_current.Value + amount, 0f, _max.Value);
        }
    }
}