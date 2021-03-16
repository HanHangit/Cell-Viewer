using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Interaction.Handlers
{
    public class InteractDragableBhvr : MonoBehaviour, InteractSelectionObject, InteractSelectionEventObject
    {
        [SerializeField]
        private Transform _root = default;
        private float _offset = 0.0f;

        private UnityEvent OnSelectionEvent = new UnityEvent();

        public void OnBeginSelection(InteractArgs args)
        {
            _offset = Vector3.Distance(args.OriginPosition, _root.position);
            if (OnSelectionEvent != null)
            {
                OnSelectionEvent.Invoke();
            }
        }

        public void OnEndSelection(InteractArgs args)
        {
        }

        public void OnUpdateSelection(InteractArgs args)
        {
            _root.position = args.OriginPosition + args.OriginLookDirection * _offset;
        }

        public void AddOnSelectionEventListener(UnityAction action)
        {
            OnSelectionEvent.AddListener(action);
        }

        public void RemoveOnSelectionEventListener(UnityAction action)
        {
            OnSelectionEvent.RemoveListener(action);
        }
    }
}
