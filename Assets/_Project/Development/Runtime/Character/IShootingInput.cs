#region

using System;

#endregion

namespace Runtime.Character
{
    public interface IShootingInput
    {
        public event Action ShotRequested;
    }
}