#region

using UnityEngine;

#endregion

namespace Runtime.Movement
{
    public class RigidbodyDirectionalRotator : IDirectionalRotator
    {
        private readonly Rigidbody _rigidbody;
        private readonly float _speed;

        public Vector3 Direction { get; private set; }

        public RigidbodyDirectionalRotator(Rigidbody rigidbody, float speed)
        {
            _rigidbody = rigidbody;
            _speed = speed;
        }

        public void Rotate(Vector3 direction)
        {
            Direction = direction.normalized;
            var lookRotation = Quaternion.LookRotation(Direction);
            var step = _speed * Time.deltaTime;

            var targetRotation = Quaternion.RotateTowards(_rigidbody.rotation, lookRotation, step);

            _rigidbody.MoveRotation(targetRotation);
        }
    }
}