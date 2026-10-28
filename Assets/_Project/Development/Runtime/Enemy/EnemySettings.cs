#region

using UnityEngine;

#endregion

namespace Runtime.Enemy
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Gameplay/EnemySettings", order = 0)]
    public class EnemySettings : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float DamageOnPlayerHit { get; private set; }
        [field: SerializeField] public float HitCooldown { get; private set; }

        [field: SerializeField] public float DirectionChangeInterval { get; private set; }
    }
}