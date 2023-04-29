using System.Collections;
using Component;
using Player.Entity;
using UnityEngine;

namespace Player.Camera
{
    public class PlayerCameraHolder : MonoBehaviour
    {
        private const int ROTATE_ANGLE = 90;
        
        [SerializeField]
        private float _rotateSpeed;
        
        private Coroutine _rotateCoroutine;
        private IPlayerView _playerView;

        public void Init(IPlayerView playerView)
        {
            _playerView = playerView;
            _playerView.OnRotate += Rotate;
        }

        private void Rotate(RotateType rotateType)
        {
            _rotateCoroutine = StartCoroutine(RotatePointToLookAt(rotateType));
        }

        private IEnumerator RotatePointToLookAt(RotateType rotateType)
        {
            for (int i = 0; i < ROTATE_ANGLE / _rotateSpeed; i++)
            {
                if (rotateType == RotateType.Right)
                {
                    transform.Rotate(0,_rotateSpeed, 0);
                } else
                {
                    transform.Rotate(0,-_rotateSpeed, 0);
                }
                yield return null;
            }
        }

        private void Update()
        {
            transform.position = _playerView.GetOwnGameObject().transform.position;
        }

        private void OnDestroy()
        {
            if (_rotateCoroutine != null)
            {
                StopCoroutine(_rotateCoroutine);
            }
            _playerView.OnRotate -= Rotate;
        }
    }
}
