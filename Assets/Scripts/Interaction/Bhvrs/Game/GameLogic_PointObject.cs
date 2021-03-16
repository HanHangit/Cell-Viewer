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
    public class GameLogic_PointObject
    {
        private IGameTask _gameTask = null;
        private InteractSelectionEventObject _selectionObject = null;

        public GameLogic_PointObject(IGameTask gameTask, InteractSelectionEventObject selectionObject)
        {
            _gameTask = gameTask;
            _selectionObject = selectionObject;
            selectionObject.AddOnSelectionEventListener(OnObjectSelectedEvent);
        }

        private void OnObjectSelectedEvent()
        {
            _gameTask.OnTaskSuccess();
            _selectionObject.RemoveOnSelectionEventListener(OnObjectSelectedEvent);
        }
    }
}
