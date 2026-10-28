#region

using Runtime.Character;
using Runtime.Controllers;
using Runtime.Enemy;
using Runtime.Level;
using Runtime.Spawners;
using UnityEngine;

#endregion

namespace Runtime.Bootstrap
{
    public class GameBoostrap : MonoBehaviour
    {
        private ControllerRunnerService _controllerRunnerService;
        private PlayerCharacterSpawner _playerCharacterSpawner;
        private EnemiesSpawner _enemiesSpawner;
        private EnemyCreationService _enemyCreationService;
        private LevelConditionsCreator _levelConditionsCreator;
        private LevelConditionsController _levelConditionsController;

        private void Awake()
        {
            RegisterAllServices();

            InitializeGame();
        }

        private void RegisterAllServices()
        {
            var controllersRunnerServicePrefab = Resources.Load<ControllerRunnerService>("Utilities/ControllersRunnerServicePrefab");
            _controllerRunnerService = Instantiate(controllersRunnerServicePrefab);

            _playerCharacterSpawner = new(_controllerRunnerService);
            _enemiesSpawner = new EnemiesSpawner(_controllerRunnerService);

            var enemyPrefab = Resources.Load<Enemy.Enemy>("Level/EnemyPrefab");
            _enemyCreationService = new(_enemiesSpawner, enemyPrefab, this);
        }

        private void InitializeGame()
        {
            var playerCharacterSettings = Resources.Load<PlayerCharacterSettings>("PlayerCharacter/PlayerCharacterSettings");
            var playerCharacter = _playerCharacterSpawner.CreatePlayerCharacterFrom(playerCharacterSettings);

            var levelSettings = Resources.Load<LevelSettings>("Level/LevelSettings");
            _enemyCreationService.SummonEnemiesWithInterval(levelSettings.EnemySettings, levelSettings.SpawnInterval,
                levelSettings.EnemySpawnPositions);

            _levelConditionsCreator = new LevelConditionsCreator(_enemyCreationService, playerCharacter);

            var winConditionFunc = _levelConditionsCreator.GetWinConditionFunc(levelSettings.WinConditions, levelSettings.WinConditionValue);
            var loseConditionFunc = _levelConditionsCreator.GetLoseConditionFunc(levelSettings.LoseConditions, levelSettings.LoseConditionValue);
            _levelConditionsController = new LevelConditionsController(winConditionFunc, loseConditionFunc);
            _levelConditionsController.Enable();

            _controllerRunnerService.Register(_levelConditionsController, () => false);
            _levelConditionsController.GameWon += LevelConditionsController_OnGameWon;
            _levelConditionsController.GameLost += LevelConditionsController_OnGameLost;

            // add restart
            // subscribe to win and lose
            // clean scene after won/lose
        }

        private void LevelConditionsController_OnGameLost()
        {
            Debug.Log("GAME LOST!");
        }

        private void LevelConditionsController_OnGameWon()
        {
            Debug.Log("GAME WON");
        }
    }
}