#region

using System;
using Cinemachine;
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
        [SerializeField] private CinemachineVirtualCamera _followCamera;

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
            ControllerRunnerService controllersRunnerServicePrefab =
                Resources.Load<ControllerRunnerService>("Utilities/ControllersRunnerServicePrefab");

            _controllerRunnerService = Instantiate(controllersRunnerServicePrefab);

            _playerCharacterSpawner = new PlayerCharacterSpawner(_controllerRunnerService);
            _enemiesSpawner = new EnemiesSpawner(_controllerRunnerService);

            Enemy.Enemy enemyPrefab = Resources.Load<Enemy.Enemy>("Level/EnemyPrefab");
            _enemyCreationService = new EnemyCreationService(_enemiesSpawner, enemyPrefab, this);
        }

        private void InitializeGame()
        {
            PlayerCharacterSettings playerCharacterSettings = Resources.Load<PlayerCharacterSettings>("PlayerCharacter/PlayerCharacterSettings");
            PlayerCharacter playerCharacter = _playerCharacterSpawner.CreatePlayerCharacterFrom(playerCharacterSettings, _followCamera);

            LevelSettings levelSettings = Resources.Load<LevelSettings>("Level/LevelSettings");

            _enemyCreationService.SummonEnemiesWithInterval(levelSettings.EnemySettings, levelSettings.SpawnInterval,
                levelSettings.EnemySpawnPositions);

            _levelConditionsCreator = new LevelConditionsCreator(_enemyCreationService, playerCharacter);

            Func<bool> winConditionFunc = _levelConditionsCreator.GetWinConditionFunc(levelSettings.WinConditions, levelSettings.WinConditionValue);

            Func<bool> loseConditionFunc =
                _levelConditionsCreator.GetLoseConditionFunc(levelSettings.LoseConditions, levelSettings.LoseConditionValue);

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