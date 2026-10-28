#region

using Runtime.Reacts;

#endregion

namespace Runtime.Health
{
    public interface IHaveHealth
    {
        public IReadonlyReactVariable<float> CurrentHealth { get; }
        public IReadonlyReactVariable<float> MaxHealth { get; }

        public void TakeDamage(float amount);
        public void Heal(float amount);
    }
}