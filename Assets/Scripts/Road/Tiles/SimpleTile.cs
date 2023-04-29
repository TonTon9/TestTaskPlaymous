using System;
using System.Collections.Generic;
using Component.Base;
using Player.Entity;
using UnityEngine;

namespace Road
{
    public class SimpleTile : BaseOnTriggerAction
    {
        public event Action OnPlayerExitTile = delegate {};
        
        [field: SerializeField]
        public Transform NextTileSpawnPoint { get; private set; }

        [field: SerializeField]
        public TileType TileType { get; private set; }

        [SerializeField]
        public List<TileType> nextTiles = new();

        protected override void ActionOnTriggerEnter(Collider collider)
        {
            if (!_isActivated &&  collider.TryGetComponent(out IPlayerView view))
            {
                _isActivated = true;
                OnPlayerExitTile?.Invoke();
            }
        }
    }
}
