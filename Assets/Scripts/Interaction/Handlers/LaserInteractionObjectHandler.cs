using System;
using HeadlessOpenVR;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Interaction
{
    public class LaserInteractionObjectHandler : InteractObjectHandlerFactory, InteractObjectHandler, InteractArgsCreator
    {
        [SerializeField]
        private LineRenderer _lineRenderer = default;
        [SerializeField]
        private LayerMask _collisionMask = default;
        [SerializeField]
        private Color _detectionColor = Color.yellow;
        [SerializeField]
        private Color _dragColor = Color.green;
        [SerializeField]
        private Color _defaultColor = Color.red;
        [SerializeField]
        private Transform _rayOrigin = default;

        public class InteractEvent : UnityEvent<List<InteractObject>> { }

        public UnityEvent<List<InteractObject>> OnHoverBeginEvent = new InteractEvent();
        public UnityEvent<List<InteractObject>> OnHoverUpdateEvent = new InteractEvent();
        public UnityEvent<List<InteractObject>> OnHoverEndEvent = new InteractEvent();

        private List<InteractObject> _currentInteractionObjects = new List<InteractObject>();

        private void Update()
        {
            var ray = new Ray(_rayOrigin.position, _rayOrigin.forward);
            var hits = Physics.RaycastAll(ray, 1000, _collisionMask);
            if (hits.Any())
            {
                OnHit(hits[0]);
                _lineRenderer.material.color = _detectionColor;
            }
            else
            {
                OnHit(null);
                _lineRenderer.material.color = _defaultColor;
            }
        }

        private void OnHit(RaycastHit? hit)
        {
            var interactObject = hit?.collider.GetComponents<InteractObject>();

            if (interactObject != null)
            {
                var newList = new List<InteractObject>();

                foreach (var o in interactObject)
                {
                    if (_currentInteractionObjects.Contains(o))
                    {
                        OnHoverUpdateEvent?.Invoke(new List<InteractObject> { o });
                    }
                    else
                    {
                        OnHoverBeginEvent?.Invoke(new List<InteractObject> { o });
                    }

                    _currentInteractionObjects.Remove(o);

                    newList.Add(o);
                }

                foreach (var end in _currentInteractionObjects)
                {
                    OnHoverEndEvent?.Invoke(new List<InteractObject> { end });
                }

                _currentInteractionObjects = newList;
            }
            else
            {
                foreach (var o in _currentInteractionObjects)
                {
                    OnHoverEndEvent?.Invoke(new List<InteractObject> { o });
                }
                _currentInteractionObjects.Clear();
            }
        }

        public void AddHoverUpdateEventListener(UnityAction<List<InteractObject>> callback)
        {
            OnHoverUpdateEvent.AddListener(callback);
        }

        public void AddHoverBeginEventListener(UnityAction<List<InteractObject>> callback)
        {
            OnHoverBeginEvent.AddListener(callback);
        }

        public void AddHoverEndEventListener(UnityAction<List<InteractObject>> callback)
        {
            OnHoverEndEvent.AddListener(callback);
        }

        public override InteractObjectHandler GetInteractObjectHandler()
        {
            return this;
        }

        public InteractArgs GetInteractArgs()
        {
            return new InteractArgs
            {
                OriginPosition = _rayOrigin.position,
                OriginLookDirection = _rayOrigin.forward
            };
        }
    }
}