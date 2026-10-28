#region

using UnityEngine;

#endregion

namespace Runtime.Enemy
{
    public interface IHitProcessor
    {
        public void ProcessHit(Collision collision);
    }
}