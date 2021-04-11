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

        private Vector3 _offsetFromCollider = Vector3.zero;

        private bool _isInSelection = false;

        private float _offset = 0.0f;

        private UnityEvent OnSelectionEvent = new UnityEvent();

        private void OnValidate()
        {
            _root = GetComponent<Transform>();
        }

        public void OnBeginSelection(InteractArgs args)
        {
            if (!_isInSelection)
            {
                _offset = Vector3.Distance(args.OriginPosition, args.HitPosition);
                _offsetFromCollider = _root.position - args.HitPosition;
                if (OnSelectionEvent != null)
                {
                    OnSelectionEvent.Invoke();
                }

                _isInSelection = true;
            }
        }

        public void OnEndSelection(InteractArgs args)
        {
            _isInSelection = false;
        }

        public void OnUpdateSelection(InteractArgs args)
        {
            float moveOffset = 0.0f;
            if (args.Offset != Vector3.zero)
            {
                moveOffset = Vector3.Dot(args.OriginLookDirection, args.Offset);
            }

            _root.position = args.OriginPosition + args.OriginLookDirection * _offset + args.OriginLookDirection * moveOffset + _offsetFromCollider;
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
