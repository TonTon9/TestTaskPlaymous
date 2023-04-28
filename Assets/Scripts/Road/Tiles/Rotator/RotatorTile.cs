using Player.Movement;
using UnityEngine;

public class RotatorTile : BaseOnTriggerAction
{
    [SerializeField]
    private RotateType _rotateType;
    protected override void ActionOnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out IPlayerView view))
        {
            view.Rotate(_rotateType);
        }
    }
}

public abstract class BaseOnTriggerAction : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        ActionOnTriggerEnter(other);
    }

    protected abstract void ActionOnTriggerEnter(Collider collider);
}

public enum RotateType
{
    Right,
    Left
}