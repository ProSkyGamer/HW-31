#region

using UnityEngine;

#endregion

namespace Runtime.Utility
{
    public class DestroyAfter : MonoBehaviour
    {
        [SerializeField] private float _destroyAfter = 5f;

        private void Awake()
        {
            Destroy(gameObject, _destroyAfter);
        }
    }
}