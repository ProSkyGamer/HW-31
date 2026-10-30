#region

using Cinemachine;
using Runtime.Character;
using Runtime.Controllers;
using Runtime.Enemy;
using Runtime.Level;
using Runtime.Spawners;
using UnityEngine;

#endregion

namespace Runtime.Game
{
    public class GameController : IController, ILevelData
    {
        private readonly PlayerCharacterSettings _playerCharacterSettings;
        private readonly PlayerCharacterSpawner _playerCharacterSpawner;

        private readonly LevelSettings _levelSettings;
        private readonly EnemyCreationService _enemyCreationService;
        private readonly LevelConditionsController _levelConditionsController;

        private readonly CinemachineVirtualCamera _followCamera;
        private PlayerCharacter _currentPlayerCharacter;

        public int SpawnedEnemies => _enemyCreationService.CreatedEnemies.Count;
        public int KilledEnemies => _enemyCreationService.DeadCount;
        public bool IsPlayerCharacterAlive => _currentPlayerCharacter != null && _currentPlayerCharacter.IsDestroyed == false;
        public float PlayerAliveTime => _currentPlayerCharacter != null ? _currentPlayerCharacter.TimeAlive : 0f;

        public GameController(PlayerCharacterSettings playerCharacterSettings, PlayerCharacterSpawner playerCharacterSpawner,
            LevelSettings levelSettings, EnemyCreationService enemyCreationService, LevelConditionsController levelConditionsController,
            CinemachineVirtualCamera followCamera)
        {
            _playerCharacterSettings = playerCharacterSettings;
            _playerCharacterSpawner = playerCharacterSpawner;
            _levelSettings = levelSettings;
            _enemyCreationService = enemyCreationService;
            _levelConditionsController = levelConditionsController;
            _followCamera = followCamera;
        }

        public void StartGame()
        {
            if (_currentPlayerCharacter != null && _currentPlayerCharacter.IsDestroyed == false)
                Object.Destroy(_currentPlayerCharacter.gameObject);

            _currentPlayerCharacter = _playerCharacterSpawner.CreatePlayerCharacterFrom(_playerCharacterSettings, _followCamera);

            _enemyCreationService.SummonEnemiesWithInterval(_levelSettings.EnemySettings, _levelSettings.SpawnInterval,
                _levelSettings.EnemySpawnPositions);

            _levelConditionsController.GameWon += LevelConditionsController_OnGameWon;
            _levelConditionsController.GameLost += LevelConditionsController_OnGameLost;
        }

        private void LevelConditionsController_OnGameLost()
        {
            Debug.Log("GAME LOST!");

            EndGame();
        }

        private void LevelConditionsController_OnGameWon()
        {
            Debug.Log("GAME WON");

            EndGame();
        }

        public void EndGame()
        {
            _enemyCreationService.StopSpawningEnemies();

            _levelConditionsController.GameWon -= LevelConditionsController_OnGameWon;
            _levelConditionsController.GameLost -= LevelConditionsController_OnGameLost;
        }

        public void Update(float deltaTime)
        {
            _levelConditionsController.CheckConditions(this);
        }
    }
}