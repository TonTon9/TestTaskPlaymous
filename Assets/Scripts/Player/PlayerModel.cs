using Player.Entity;
using UniRx;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerModel : IPlayerModel
    {
        public ReactiveProperty<int> Health { get; }
        
        public ReactiveProperty<int> MaxHealth { get; }
        
        public ReactiveProperty<float> Speed { get; }
        
        public ReactiveProperty<float> MaxSpeed { get; }
        public ReactiveProperty<bool> IsAlive { get; }
        public ReactiveProperty<bool> IsImmune { get; }


        public PlayerModel(int health, float speed)
        {
            Health = new ReactiveProperty<int>(health);
            MaxHealth = new ReactiveProperty<int>(health);
            Speed = new ReactiveProperty<float>(speed);
            MaxSpeed = new ReactiveProperty<float>(speed);
            IsAlive = new ReactiveProperty<bool>(true);
            IsImmune = new ReactiveProperty<bool>(false);
        }

        public void SetHealthToMax()
        {
            Health.Value = MaxHealth.Value;
        }

        public void SetHealth(int newValue)
        {
            if (newValue > MaxHealth.Value)
            {
                newValue = MaxHealth.Value;
            }
            if (newValue < 0)
            {
                Health.Value = 0;
            } else
            {
                Health.Value = newValue;
            }
        }

        public void SetSpeed(float newValue)
        {
            if (newValue > MaxSpeed.Value)
            {
                newValue = MaxSpeed.Value;
            }
            if (newValue < 0)
            {
                Speed.Value = 0;
            } else
            {
                Speed.Value = newValue;
            }
        }
        
        public void SetMaxSpeed(float speed)
        {
            if (speed < 0)
            {
                Debug.LogWarning("You want to set max speed to zero");
                return;
            }
            MaxSpeed.Value = speed;
            SetSpeed(MaxSpeed.Value);
        }
    }
}
