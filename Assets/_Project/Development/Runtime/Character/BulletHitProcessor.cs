#region

using System;
using Runtime.Enemy;
using Runtime.Health;
using UnityEngine;

#endregion

namespace Runtime.Character
{
    public class BulletHitProcessor : IHitProcessor
    {
        private readonly float _damageOnHit;
        private readonly Action _destroyAction;

        public BulletHitProcessor(float damageOnHit, Action destroyAction)
        {
            _damageOnHit = damageOnHit;
            _destroyAction = destroyAction;
        }

        public void ProcessHit(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IHaveHealth healthComponent))
            {
                healthComponent.TakeDamage(_damageOnHit);
                _destroyAction?.Invoke();
            }
        }
    }
}