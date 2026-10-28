#region

using System.Collections.Generic;
using Runtime.Enemy;
using UnityEngine;

#endregion

namespace Runtime.Level
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Gameplay/LevelSettings", order = 0)]
    public class LevelSettings : ScriptableObject
    {
        [field: SerializeField] public EnemySettings EnemySettings { get; private set; }
        [field: SerializeField] public List<Vector3> EnemySpawnPositions { get; private set; }
        [field: SerializeField] public float SpawnInterval { get; private set; }

        [field: SerializeField] public LevelWinConditions WinConditions { get; private set; }
        [field: SerializeField] public int WinConditionValue { get; private set; }
        [field: SerializeField] public LevelLoseConditions LoseConditions { get; private set; }
        [field: SerializeField] public int LoseConditionValue { get; private set; }
    }
}