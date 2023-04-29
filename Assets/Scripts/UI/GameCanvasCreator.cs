using Game;
using UI.DamageEffect;
using UnityEngine;

namespace Creator
{
    public class GameCanvasCreator : MonoBehaviour, ICreator    
    {
        public bool IsInitialize { get; private set; }
        
        public void Create()
        {
            CreateDamageEffect();
            IsInitialize = true;
        }

        private void CreateDamageEffect()
        {
            var playerCreatorPrefab = Resources.Load<GameObject>("UI/DamageEffect");
            var roadGeneratorInstance = Instantiate(playerCreatorPrefab, transform).GetComponent<IDamageEffectUI>();
            roadGeneratorInstance.Init(PlayerCreator.Instance.PlayerView.PlayerModel.Health);
        }
    }

}

