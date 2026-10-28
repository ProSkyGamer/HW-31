#region

using UnityEngine;

#endregion

namespace Runtime.Character
{
    [CreateAssetMenu(fileName = "PlayerCharacterSettings", menuName = "Gameplay/PlayerCharacter/PlayerCharacterSettings", order = 0)]
    public class PlayerCharacterSettings : ScriptableObject
    {
        [field: SerializeField] public PlayerCharacter Prefab { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public Vector3 SpawnPoint { get; private set; }

        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public Bullet BulletPrefab { get; private set; }
        [field: SerializeField] public float BulletDamage { get; private set; }
        [field: SerializeField] public float BulletSpeed { get; private set; }
    }
}