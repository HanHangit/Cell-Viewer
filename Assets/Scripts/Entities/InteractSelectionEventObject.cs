using UnityEngine.Events;

namespace Assets.Scripts.Interaction
{
    public interface InteractSelectionEventObject
    {
        void AddOnSelectionEventListener(UnityAction action);
        void RemoveOnSelectionEventListener(UnityAction action);
    }
}
