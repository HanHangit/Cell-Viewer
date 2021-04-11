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
        private List<GameLogic_PointObject> _pointOjects = new List<GameLogic_PointObject>();

        public Quest_PointBhvr(Quest quest, IGameEntities gameEntities)
        {
            var obj = gameEntities.GetEntityBhvrs(quest.GetTargetEntity());

            if (obj != null)
            {
                foreach (var entityBhvr in obj)
                {
                    _pointOjects.Add(new GameLogic_PointObject(this, entityBhvr.GetComponentInChildren<InteractSelectionEventObject>()));
                }
            }
        }

        public override void OnTaskSuccess()
        {
            base.OnTaskSuccess();

            foreach (var item in _pointOjects)
            {
                item.RemoveListener();
            }
        }
    }
}
