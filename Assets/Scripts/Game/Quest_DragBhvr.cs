﻿using System;
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
    public class Quest_DragBhvr : QuestBhvr
    {
        private GameObject _object = null;
        private GameLogic_SamePosition _samePosition = null;

        public Quest_DragBhvr(Quest quest, IGameEntities gameEntities)
        {
            _samePosition = new GameLogic_SamePosition(new GamePositionAdapter(gameEntities.GetEntityBhvr(quest.GetTargetEntity())), 4.0f);
        }

        private class GamePositionAdapter : IObjectPosition
        {
            private QuestEntityBhvr _obj = null;
            public GamePositionAdapter(QuestEntityBhvr gameObj)
            {
                _obj = gameObj;
            }

            public Vector3 GetPosition()
            {
                return _obj.transform.position;
            }
        }
    }

    public interface IGameEntities
    {
        QuestEntityBhvr GetEntityBhvr(Entity entity);
    }
}