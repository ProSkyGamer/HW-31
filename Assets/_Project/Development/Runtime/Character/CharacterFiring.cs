#region

using Runtime.Movement;
using UnityEngine;

#endregion

namespace Runtime.Character
{
    public class CharacterFiring : IShootingController
    {
        private readonly Bullet _bulletPrefab;
        private readonly Transform _firingPositionTransform;
        private readonly float _bulletSpeed;
        private readonly float _bulletDamage;
        private readonly IRotationDirection _rotationDirection;

        public CharacterFiring(Bullet bulletPrefab, Transform firingPositionTransform, float bulletDamage, float bulletSpeed,
            IRotationDirection rotationDirection)
        {
            _bulletPrefab = bulletPrefab;
            _firingPositionTransform = firingPositionTransform;
            _bulletSpeed = bulletSpeed;
            _rotationDirection = rotationDirection;
            _bulletDamage = bulletDamage;
        }

        public void Shoot()
        {
            var spawnPosition = _firingPositionTransform.position;
            var lookRotation = _rotationDirection.Direction;
            var spawnRotation = lookRotation.magnitude < 0.05f ? Quaternion.identity : Quaternion.LookRotation(lookRotation);

            var newBullet = Object.Instantiate(_bulletPrefab, spawnPosition, spawnRotation);
            var hitProcessor = new BulletHitProcessor(_bulletDamage, () => Object.Destroy(newBullet.gameObject));
            newBullet.Initialize(lookRotation, _bulletSpeed, hitProcessor);
        }
    }
}