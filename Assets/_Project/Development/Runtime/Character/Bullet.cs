#region

using Runtime.Enemy;
using UnityEngine;

#endregion

namespace Runtime.Character
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _flightDirection;
        private float _speed;
        private IHitProcessor _hitProcessor;

        public void Initialize(Vector3 direction, float speed, IHitProcessor hitProcessor)
        {
            _flightDirection = direction;
            _speed = speed;
            _hitProcessor = hitProcessor;
        }

        private void Update()
        {
            transform.position += _flightDirection * (_speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision other)
        {
            _hitProcessor.ProcessHit(other);
        }
    }
}