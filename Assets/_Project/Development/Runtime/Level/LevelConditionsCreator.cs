#region

using System;
using Runtime.Character;
using Runtime.Enemy;
using UnityEngine;

#endregion

namespace Runtime.Level
{
    public class LevelConditionsCreator
    {
        private readonly EnemyCreationService _enemyCreationService;
        private readonly PlayerCharacter _playerCharacter;

        public LevelConditionsCreator(EnemyCreationService enemyCreationService, PlayerCharacter playerCharacter)
        {
            _enemyCreationService = enemyCreationService;
            _playerCharacter = playerCharacter;
        }

        public Func<bool> GetWinConditionFunc(LevelWinConditions winConditions, int parameter)
        {
            switch (winConditions)
            {
                default:
                    throw new NotImplementedException($"Win condition {winConditions} not implemented");
                case LevelWinConditions.KillEnemies:
                    return () => _enemyCreationService.DeadCount >= parameter;
                case LevelWinConditions.LiveForSeconds:
                    return () => _playerCharacter.IsDestroyed == false && _playerCharacter.TimeAlive >= parameter;
            }
        }

        public Func<bool> GetLoseConditionFunc(LevelLoseConditions loseConditions, int parameter)
        {
            switch (loseConditions)
            {
                default:
                    throw new NotImplementedException($"Lose condition {loseConditions} not implemented");
                case LevelLoseConditions.PlayerCharacterDeath:
                    return () => _playerCharacter.IsDestroyed;
                case LevelLoseConditions.AllowEnemiesToSpawn:
                    return () => _enemyCreationService.CreatedEnemies.Count >= parameter;
            }
        }
    }
}