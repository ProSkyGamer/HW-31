#region

using System;
using Runtime.Controllers;

#endregion

namespace Runtime.Level
{
    public class LevelConditionsController : Controller
    {
        public event Action GameWon;
        public event Action GameLost;

        private readonly Func<bool> _winCondition;
        private readonly Func<bool> _loseCondition;

        public LevelConditionsController(Func<bool> winCondition, Func<bool> loseCondition)
        {
            _winCondition = winCondition ?? throw new NullReferenceException("Win condition is null");
            _loseCondition = loseCondition ?? throw new NullReferenceException("Lose condition is null");
        }

        protected override void UpdateInternal(float deltaTime)
        {
            if (_winCondition())
                GameWon?.Invoke();
            else if (_loseCondition())
                GameLost?.Invoke();
        }
    }
}