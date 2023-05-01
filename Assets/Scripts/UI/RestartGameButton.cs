using Component.Base;
using Cysharp.Threading.Tasks;
using Game;

namespace UI
{
    public class RestartGameButton : BaseButtonComponent
    {
        protected override async void OnButtonClickAction()
        {
            await UniTask.WaitUntil(() => GameStateManager.Instance != null);
            
            GameStateManager.Instance.RestartGame();
        }
    }
}
