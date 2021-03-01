namespace Assets.Scripts.Interaction
{
    public interface InteractObject
    {
        void BeginTrigger(InteractArgs args);
        void EndTrigger(InteractArgs args);
        void HoverBegin(InteractArgs args);
        void HoverUpdate(InteractArgs args);
        void HoverEnd(InteractArgs args);
    }
}
