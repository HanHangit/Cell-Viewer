using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Interaction;
using Assets.Scripts.Interaction.Bhvrs;
using Assets.Scripts.Interaction.Bhvrs.Game;
using Assets.Scripts.Interaction.Handlers;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Quest_PointBhvr : QuestBhvr
    {
        private GameLogic_PointObject _pointOject = null;

        public Quest_PointBhvr(Quest quest, IGameEntities gameEntities)
        {
            var obj = gameEntities.GetEntityBhvr(quest.GetTargetEntity());
            if(obj != null)
            {
                _pointOject = new GameLogic_PointObject(this, obj.GetComponentInChildren<InteractSelectionEventObject>());
            }
        }
    }
}
