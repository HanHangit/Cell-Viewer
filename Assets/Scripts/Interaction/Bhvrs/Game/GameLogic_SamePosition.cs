using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Interaction.Bhvrs.Game;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Scripts.Interaction
{
    public class GameLogic_SamePosition
    {
        private IObjectPosition _originObject;
        private IObjectPosition _dragableObject = null;
        private IGameTask _gameTask = null;
        private float _positionOffset = 0.0f;

        public GameLogic_SamePosition(IObjectPosition originObject, IGameTask gameTask, float positionOffset)
        {
            _originObject = originObject;
            _gameTask = gameTask;
            _positionOffset = positionOffset;
        }


        public GameLogic_SamePosition(IObjectPosition originObject, float positionOffset) : this(originObject, null, positionOffset)
        {
        }

        public void SetGameTask(IGameTask gameTask)
        {
            _gameTask = gameTask;
        }

        public bool IsTargetReached()
        {
            float magnitude = (_dragableObject.GetPosition() - _originObject.GetPosition()).magnitude;

            return magnitude <= _positionOffset;
        }

        public void Update()
        {
            if (_gameTask != null)
            {
                if (IsTargetReached())
                {
                    _gameTask.OnTaskSuccess();
                    _gameTask = null;
                }
            }
        }

        public void SetDragableObject(IObjectPosition targetObject)
        {
            _dragableObject = targetObject;
        }
    }
}
