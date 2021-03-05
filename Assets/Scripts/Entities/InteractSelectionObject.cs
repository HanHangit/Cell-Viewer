namespace Assets.Scripts.Interaction
{
    public interface InteractSelectionObject
    {
        void OnBeginSelection(InteractArgs args);
        void OnEndSelection(InteractArgs args);
        void OnUpdateSelection(InteractArgs args);
    }
}
