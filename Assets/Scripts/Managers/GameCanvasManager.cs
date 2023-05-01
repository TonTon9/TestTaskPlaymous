using Game;
using UI.DamageEffect;
using UnityEngine;

namespace Creator
{
    public class GameCanvasManager : BaseManager<GameCanvasManager>, IManager
    {
        private const string PLAYER_CREATOR_PREFAB_PATH = "UI/DamageEffect";
        public override void Initialize()
        {
            base.Initialize();
            CreateDamageEffect();
            IsInitialize = true;
        }

        private void CreateDamageEffect()
        {
            var playerCreatorPrefab = Resources.Load<GameObject>(PLAYER_CREATOR_PREFAB_PATH);
            var roadGeneratorInstance = Instantiate(playerCreatorPrefab, transform).GetComponent<IDamageEffectUI>();
            roadGeneratorInstance.Init(PlayerManager.Instance.PlayerView.PlayerModel.Health);
        }
    }
}
