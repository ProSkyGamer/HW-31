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
            public ControllerRegistration(Controller controller, Func<bool> destructionReason)
            {
                Controller = controller;
                DestructionReason = destructionReason;
            }

            public Controller Controller { get; }
            public Func<bool> DestructionReason { get; }
        }

        private readonly List<ControllerRegistration> _runningControllers = new();

        public void Register(Controller controller, Func<bool> destructionReason)
        {
            _runningControllers.Add(new(controller, destructionReason));
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            for (var i = 0; i < _runningControllers.Count; i++)
            {
                var controllerRegistration = _runningControllers[i];

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