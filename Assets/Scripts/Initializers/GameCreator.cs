using Cysharp.Threading.Tasks;
using Road;
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
            await SpawnAndStartRoadGenerator();
        }

        private UniTask SpawnAndStartRoadGenerator()
        {
            var roadGeneratorPrefab = Resources.Load<GameObject>("GameScene/RoadGenerator");
            var roadGeneratorInstance = Instantiate(roadGeneratorPrefab).GetComponent<IRoadGenerator>();
            roadGeneratorInstance.GenerateRoad();
            return UniTask.WaitUntil(() => roadGeneratorInstance.IsInitialize);
        }
    }
}

