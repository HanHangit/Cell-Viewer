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
using UnityEngine.Events;

namespace Assets.Scripts.Game
{
    public class Quest_DragBhvr : QuestBhvr
    {
        private GameObject _object = null;
        private GameLogic_SamePosition _samePosition = null;

        public Quest_DragBhvr(Quest quest, IGameEntities gameEntities)
        {
            var targetEntity = gameEntities.GetEntityBhvr(quest.GetTargetEntity());
            _samePosition = new GameLogic_SamePosition(new GamePositionAdapter(targetEntity), 0.05f);
            _samePosition.SetGameTask(this);
            _samePosition.SetDragableObject(new GamePositionAdapter(targetEntity, true));
        }

        public override void Update()
        {
            _samePosition.Update();
        }

        private class GamePositionAdapter : IObjectPosition
        {
            private QuestEntityBhvr _obj = null;
            private bool _isTarget = false;

            public GamePositionAdapter(QuestEntityBhvr gameObj, bool isTarget = false)
            {
                _obj = gameObj;
                _isTarget = isTarget;
            }

            public Vector3 GetPosition()
            {
                if (_isTarget)
                {
                    return _obj.GetPosition();
                }
                else
                {
                    return _obj.GetTargetDragPosition();
                }
            }
        }
    }
}
