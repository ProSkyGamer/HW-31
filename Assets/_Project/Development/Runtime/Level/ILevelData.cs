namespace Runtime.Level
{
    public interface ILevelData
    {
        public int SpawnedEnemies { get; }
        public int KilledEnemies { get; }

        public bool IsPlayerCharacterAlive { get; }
        public float PlayerAliveTime { get; }
    }
}