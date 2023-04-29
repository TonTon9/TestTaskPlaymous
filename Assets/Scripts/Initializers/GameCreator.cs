using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class GameCreator : MonoBehaviour
    {
        private void Awake()
        {
            SetUpGameScene();
        }

        private async void SetUpGameScene()
        {
            await CreateEntityAndWaitUntilInitialize("GameScene/RoadGenerator");
            await CreateEntityAndWaitUntilInitialize("GameScene/PlayerCreator");
            await CreateEntityAndWaitUntilInitialize("GameScene/GameCanvas");
        }

        private UniTask CreateEntityAndWaitUntilInitialize(string entityPath)
        {
            var playerCreatorPrefab = Resources.Load<GameObject>(entityPath);
            var roadGeneratorInstance = Instantiate(playerCreatorPrefab).GetComponent<ICreator>();
            roadGeneratorInstance.Create();
            return UniTask.WaitUntil(() => roadGeneratorInstance.IsInitialize);
        }
    }
}

