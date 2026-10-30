#region

using System;

#endregion

namespace Runtime.Level
{
    public class LevelConditionsController
    {
        public event Action GameWon;
        public event Action GameLost;

        private readonly Func<ILevelData, bool> _winCondition;
        private readonly Func<ILevelData, bool> _loseCondition;

        public LevelConditionsController(Func<ILevelData, bool> winCondition, Func<ILevelData, bool> loseCondition)
        {
            _winCondition = winCondition ?? throw new NullReferenceException("Win condition is null");
            _loseCondition = loseCondition ?? throw new NullReferenceException("Lose condition is null");
        }

        public void CheckConditions(ILevelData levelData)
        {
            if (_winCondition(levelData))
                GameWon?.Invoke();
            else if (_loseCondition(levelData))
                GameLost?.Invoke();
        }
    }
}