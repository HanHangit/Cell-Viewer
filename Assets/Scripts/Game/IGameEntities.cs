using Assets.Scripts.Interaction.Bhvrs;
using System.Collections.Generic;

namespace Assets.Scripts.Game
{
    public interface IGameEntities
    {
        List<QuestEntityBhvr> GetEntityBhvrs(Entity entity);
    }
}
