using System;
using Boosters;
using Component;

namespace Player.Entity
{
    public interface IPlayerPresenter : IDamagable
    {
        event Action<RotateType> OnRotate;
        
        void ApplyBooster(BoosterType boosterType);

        void Rotate(RotateType rotateType);
    }
}
