using UniRx;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovement : IMove
    {
        private const float GRAVITY_POWER = 10f;
        private const float JUMP_POWER = 5f;
        
        private CharacterController _characterController;
        private float _moveSpeed;
        private Transform _playerTransform;
        private Vector3 _direction;
        private float _gravityForce;
        private int _countOfJumps;

        public PlayerMovement(GameObject playerGameObject, ReactiveProperty<float> speed)
        {
            _playerTransform = playerGameObject.transform;
            SetSpeed(speed.Value);
            speed.Subscribe(SetSpeed);
            _characterController = playerGameObject.GetComponent<CharacterController>();
        }

        private void SetSpeed(float newSpeed)
        {
            _moveSpeed = newSpeed;
        }

        public void Move()
        {
            _direction = Vector3.zero;
            _direction.z = 1;
            
            _direction = Vector3.forward * _moveSpeed;

            _direction.y = _gravityForce;
            _direction = _playerTransform.TransformDirection(_direction);   
            
            _characterController.Move(_direction * Time.deltaTime);
            CustomGravity();
        }

        public void Jump()
        {
            if (_characterController.isGrounded)
            {
                _countOfJumps = 0;
            }

            if (_countOfJumps < 2)
            {
                _gravityForce = JUMP_POWER;
                _countOfJumps++;
            }
        }
        
        private void CustomGravity()
        {
            if (!_characterController.isGrounded)
            {
                _gravityForce -= GRAVITY_POWER * Time.deltaTime;
            } else
            {
                _gravityForce = -1f;
            }
        }
    }
}
