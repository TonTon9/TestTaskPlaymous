using Component.Base;
using Player.Entity;
using Road;
using UnityEngine;

namespace Boosters
{
    public class BoosterGameObject : BaseOnTriggerAction
    {
        [field: SerializeField]
        public BoosterType BoosterType { get; private set; }

        private SimpleTile _tile;

        public void Init(SimpleTile tile)
        {
            _tile = tile;
            _tile.OnPlayerExitTile += DestroyBooster;
        }

        private void DestroyBooster(SimpleTile tile)
        {
            Destroy(gameObject);
        }

        protected override void ActionOnTriggerEnter(Collider collider)
        {
            if (!_isActivated &&  collider.TryGetComponent(out IPlayerView view))
            {
                _isActivated = true;
                view.PlayerPresenter.ApplyBooster(BoosterType);
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (_tile != null)
            {
                _tile.OnPlayerExitTile -= DestroyBooster;
            }
        }
    }
}
