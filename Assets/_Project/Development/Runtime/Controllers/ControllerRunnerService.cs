#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Runtime.Controllers
{
    public class ControllerRunnerService : MonoBehaviour
    {
        private class ControllerRegistration
        {
            public ControllerRegistration(IController controller, Func<bool> destructionReason)
            {
                Controller = controller;
                DestructionReason = destructionReason;
            }

            public IController Controller { get; }
            public Func<bool> DestructionReason { get; }
        }

        private readonly List<ControllerRegistration> _runningControllers = new();

        public void Register(IController controller, Func<bool> destructionReason)
        {
            _runningControllers.Add(new(controller, destructionReason));
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            for (int i = 0; i < _runningControllers.Count; i++)
            {
                ControllerRegistration controllerRegistration = _runningControllers[i];

                if (controllerRegistration.DestructionReason != null)
                    if (controllerRegistration.DestructionReason())
                    {
                        _runningControllers.RemoveAt(i);

                        i--;

                        continue;
                    }

                controllerRegistration.Controller.Update(deltaTime);
            }
        }
    }
}