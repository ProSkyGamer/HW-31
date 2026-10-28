#region

using System;
using Runtime.Controllers;
using UnityEngine;

#endregion

namespace Runtime.Character
{
    public class CharacterShootingInputController : Controller, IShootingInput
    {
        public event Action ShotRequested;

        private const KeyCode FireKeyCode = KeyCode.Mouse0;

        protected override void UpdateInternal(float deltaTime)
        {
            if (Input.GetKeyDown(FireKeyCode))
                ShotRequested?.Invoke();
        }
    }
}