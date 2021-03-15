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

        public class InteractEvent : UnityEvent<List<InteractHoverObject>> { }

        public UnityEvent<List<InteractHoverObject>> OnHoverBeginEvent = new InteractEvent();
        public UnityEvent<List<InteractHoverObject>> OnHoverUpdateEvent = new InteractEvent();
        public UnityEvent<List<InteractHoverObject>> OnHoverEndEvent = new InteractEvent();

        private List<InteractHoverObject> _currentHoverObjects = new List<InteractHoverObject>();
        private List<InteractSelectionObject> _currentSelectionObjects = new List<InteractSelectionObject>();

        private void Update()
        {
            HoverUpdate();

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

        private void HoverUpdate()
        {
            foreach (var interactObject in _currentHoverObjects)
            {
                OnHoverUpdateEvent?.Invoke(new List<InteractHoverObject> { interactObject });
            }
        }

        private void SetSelectionObjects(InteractSelectionObject[] selectionObjects)
        {
            if (selectionObjects != null)
            {
                _currentSelectionObjects.Clear();
                _currentSelectionObjects.AddRange(selectionObjects);
            }
            else
            {
                foreach (var o in _currentHoverObjects)
                {
                    OnHoverEndEvent?.Invoke(new List<InteractHoverObject> { o });
                }
                _currentSelectionObjects.Clear();
            }
        }

        private void SetHoverObjects(InteractHoverObject[] hoverObjects)
        {
            if (hoverObjects != null)
            {
                var newList = new List<InteractHoverObject>();

                foreach (var o in hoverObjects)
                {
                    if (!_currentHoverObjects.Contains(o))
                    {
                        OnHoverBeginEvent?.Invoke(new List<InteractHoverObject> { o });
                    }

                    _currentHoverObjects.Remove(o);

                    newList.Add(o);
                }

                foreach (var end in _currentHoverObjects)
                {
                    OnHoverEndEvent?.Invoke(new List<InteractHoverObject> { end });
                }

                _currentHoverObjects = newList;
            }
            else
            {
                foreach (var o in _currentHoverObjects)
                {
                    OnHoverEndEvent?.Invoke(new List<InteractHoverObject> { o });
                }
                _currentHoverObjects.Clear();
            }
        }

        private void OnHit(RaycastHit? hit)
        {
            var hoverObjects = hit?.collider.GetComponents<InteractHoverObject>();
            var selectionObjects = hit?.collider.GetComponents<InteractSelectionObject>();
            SetHoverObjects(hoverObjects);
            SetSelectionObjects(selectionObjects);
        }

        public void AddHoverUpdateEventListener(UnityAction<List<InteractHoverObject>> callback)
        {
            OnHoverUpdateEvent.AddListener(callback);
        }

        public void AddHoverBeginEventListener(UnityAction<List<InteractHoverObject>> callback)
        {
            OnHoverBeginEvent.AddListener(callback);
        }

        public void AddHoverEndEventListener(UnityAction<List<InteractHoverObject>> callback)
        {
            OnHoverEndEvent.AddListener(callback);
        }

        public List<InteractSelectionObject> GetCurrentSelectionObjects()
        {
            return _currentSelectionObjects;
        }

        public override InteractObjectHandler CreateInteractObjectHandler()
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