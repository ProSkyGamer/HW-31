#region

using UnityEngine;

#endregion

namespace Runtime.Movement
{
    public class RigidbodyDirectionalMover : IDirectionalMover
    {
        private readonly Rigidbody _rigidbody;
        private readonly float _speed;


        public Vector3 Direction { get; private set; }

        public RigidbodyDirectionalMover(Rigidbody rigidbody, float speed)
        {
            _rigidbody = rigidbody;
            _speed = speed;
        }

        public void Move(Vector3 direction)
        {
            Direction = direction.normalized;
            var movePosition = _rigidbody.position + Direction * (Time.deltaTime * _speed);
            _rigidbody.MovePosition(movePosition);
        }
    }
}