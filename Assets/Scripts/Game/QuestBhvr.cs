using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Interaction.Bhvrs.Game;
using Assets.Scripts.Interaction.Handlers;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Game
{
    public abstract class QuestBhvr : IGameTask
    {
        private GenericEvent<QuestProgressArgs> _questCompletedEvent = new GenericEvent<QuestProgressArgs>();

        public void AddQuestCompletedEventListener(UnityAction<QuestProgressArgs> action)
        {
            _questCompletedEvent.AddEventListener(action);
        }

        public void OnTaskSuccess()
        {
            _questCompletedEvent.InvokeEvent(new QuestProgressArgs());
        }

        public virtual void Update()
        {

        }
    }
}
