#region

using Runtime.Controllers;
using Runtime.Health;
using UnityEngine;

#endregion

namespace Runtime.Enemy
{
    public class EnemyHitProcessor : Controller, IHitProcessor
    {
        private readonly float _damageOnHit;
        private readonly float _hitCooldown;
        private float _cooldown;

        private bool IsHitCooldownUp => _cooldown <= 0f;

        public EnemyHitProcessor(float damageOnHit, float hitCooldown)
        {
            _damageOnHit = damageOnHit;
            _hitCooldown = hitCooldown;
        }

        public void ProcessHit(Collision collision)
        {
            if (IsHitCooldownUp == false)
                return;


            if (collision.gameObject.TryGetComponent(out IHaveHealth healthComponent))
            {
                healthComponent.TakeDamage(_damageOnHit);
                _cooldown = _hitCooldown;
            }
        }

        protected override void UpdateInternal(float deltaTime)
        {
            if (_cooldown > 0)
                _cooldown -= deltaTime;
        }
    }
}