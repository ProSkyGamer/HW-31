#region

using System;

#endregion

namespace Runtime.Level
{
    public class LevelConditionsCreator
    {
        public Func<ILevelData, bool> GetWinConditionFunc(LevelWinConditions winConditions, int parameter)
        {
            switch (winConditions)
            {
                default:
                    throw new NotImplementedException($"Win condition {winConditions} not implemented");

                case LevelWinConditions.KillEnemies:
                    return (levelData) => levelData.KilledEnemies >= parameter;

                case LevelWinConditions.LiveForSeconds:
                    return (levelData) => levelData.IsPlayerCharacterAlive && levelData.PlayerAliveTime >= parameter;
            }
        }

        public Func<ILevelData, bool> GetLoseConditionFunc(LevelLoseConditions loseConditions, int parameter)
        {
            switch (loseConditions)
            {
                default:
                    throw new NotImplementedException($"Lose condition {loseConditions} not implemented");

                case LevelLoseConditions.PlayerCharacterDeath:
                    return (levelData) => levelData.IsPlayerCharacterAlive == false;

                case LevelLoseConditions.AllowEnemiesToSpawn:
                    return (levelData) => levelData.SpawnedEnemies >= parameter;
            }
        }
    }
}