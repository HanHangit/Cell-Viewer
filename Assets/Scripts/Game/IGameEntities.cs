using Assets.Scripts.Interaction.Bhvrs;

namespace Assets.Scripts.Game
{
    public interface IGameEntities
    {
        QuestEntityBhvr GetEntityBhvr(Entity entity);
    }
}
