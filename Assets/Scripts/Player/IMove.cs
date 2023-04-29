using Component;

namespace Player.Movement
{
    public interface IMove
    {
        void Move();
        void Jump();

        void Rotate(RotateType rotateType);
    }
}
