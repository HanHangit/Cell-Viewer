namespace Assets.Scripts.Interaction
{
    public interface InteractHoverObject
    {
        void OnHoverBegin(InteractArgs args);
        void OnHoverUpdate(InteractArgs args);
        void OnHoverEnd(InteractArgs args);
    }
}
