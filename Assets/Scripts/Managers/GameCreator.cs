using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class GameCreator : MonoBehaviour
    {
        [SerializeField]
        private GameObject _loadingCanvas;
        
        private void Awake()
        {
            SetUpGameScene();
        }

        private async void SetUpGameScene()
        {
            await CreateEntityAndWaitUntilInitialize("GameScene/RoadGenerator");
            await CreateEntityAndWaitUntilInitialize("GameScene/BoosterManager");
            await CreateEntityAndWaitUntilInitialize("GameScene/PlayerCreator");
            await CreateEntityAndWaitUntilInitialize("GameScene/GameCanvas");
            await CreateEntityAndWaitUntilInitialize("GameScene/GameStateManager");
            _loadingCanvas.SetActive(false);
        }

        private UniTask CreateEntityAndWaitUntilInitialize(string entityPath)
        {
            var playerCreatorPrefab = Resources.Load<GameObject>(entityPath);
            var roadGeneratorInstance = Instantiate(playerCreatorPrefab).GetComponent<IManager>();
            roadGeneratorInstance.Initialize();
            return UniTask.WaitUntil(() => roadGeneratorInstance.IsInitialize);
        }
    }
}

