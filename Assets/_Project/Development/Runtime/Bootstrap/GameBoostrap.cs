#region

using System;
using Cinemachine;
using Runtime.Character;
using Runtime.Controllers;
using Runtime.Enemy;
using Runtime.Game;
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
        private GameController _gameController;

        private void Awake()
        {
            RegisterAllServices();

            InitializeGame();
        }

        private void RegisterAllServices()
        {
            ControllerRunnerService controllersRunnerServicePrefab =
                Resources.Load<ControllerRunnerService>("Utilities/ControllersRunnerServicePrefab");

            PlayerCharacterSettings playerCharacterSettings = Resources.Load<PlayerCharacterSettings>("PlayerCharacter/PlayerCharacterSettings");

            LevelSettings levelSettings = Resources.Load<LevelSettings>("Level/LevelSettings");

            _controllerRunnerService = Instantiate(controllersRunnerServicePrefab);

            _playerCharacterSpawner = new PlayerCharacterSpawner(_controllerRunnerService);
            _enemiesSpawner = new EnemiesSpawner(_controllerRunnerService);

            Enemy.Enemy enemyPrefab = Resources.Load<Enemy.Enemy>("Level/EnemyPrefab");
            _enemyCreationService = new EnemyCreationService(_enemiesSpawner, enemyPrefab, this);

            _levelConditionsCreator = new LevelConditionsCreator();

            Func<ILevelData, bool> winConditionFunc =
                _levelConditionsCreator.GetWinConditionFunc(levelSettings.WinConditions, levelSettings.WinConditionValue);

            Func<ILevelData, bool> loseConditionFunc =
                _levelConditionsCreator.GetLoseConditionFunc(levelSettings.LoseConditions, levelSettings.LoseConditionValue);

            _levelConditionsController = new LevelConditionsController(winConditionFunc, loseConditionFunc);

            _gameController = new GameController(playerCharacterSettings, _playerCharacterSpawner, levelSettings, _enemyCreationService,
                _levelConditionsController, _followCamera);

            _controllerRunnerService.Register(_gameController, () => false);
        }

        private void InitializeGame()
        {
            _gameController.StartGame();
        }
    }
}