using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Interaction.Bhvrs
{
    public class CameraRayCreator : InteractObjectHandlerFactory, InteractObjectHandler, InteractArgsCreator
    {
        [SerializeField]
        private LineRenderer _lineRenderer = default;
        [SerializeField]
        private Camera _camera = default;
        [SerializeField]
        private LayerMask _collisionMask = default;
        [SerializeField]
        private Color _detectionColor = Color.yellow;
        [SerializeField]
        private Color _dragColor = Color.green;
        [SerializeField]
        private Color _defaultColor = Color.red;

        private RaycastHit _currentHit = default;
        private Ray _lastRay = default;

        public class InteractEvent : UnityEvent<List<InteractHoverObject>> { }

        public UnityEvent<List<InteractHoverObject>> OnHoverBeginEvent = new InteractEvent();
        public UnityEvent<List<InteractHoverObject>> OnHoverUpdateEvent = new InteractEvent();
        public UnityEvent<List<InteractHoverObject>> OnHoverEndEvent = new InteractEvent();
        public UnityEvent<bool> OnActiveChanged = new GenericEvent<bool>();

        private List<InteractHoverObject> _currentHoverObjects = new List<InteractHoverObject>();
        private List<InteractSelectionObject> _currentSelectionObjects = new List<InteractSelectionObject>();

        public void CreateRaycast(Vector2 vector2)
        {
            var screenPoint = new Vector3(vector2.x * _camera.pixelWidth, vector2.y * _camera.pixelHeight, _camera.nearClipPlane);
            var ray = _camera.ScreenPointToRay(screenPoint);
            ray.origin -= _camera.transform.forward * 0.01f;
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPositions(new Vector3[] { ray.origin, ray.origin + ray.direction * _camera.farClipPlane });
            _lastRay = ray;

            UpdateRayHits(ray.origin, ray.direction);
        }

        private void HoverUpdate()
        {
            foreach (var interactObject in _currentHoverObjects)
            {
                OnHoverUpdateEvent?.Invoke(new List<InteractHoverObject> { interactObject });
            }
        }

        private void UpdateRayHits(Vector3 origin, Vector3 direction)
        {
            HoverUpdate();

            var ray = new Ray(origin, direction);
            var hits = Physics.RaycastAll(ray, 1000, _collisionMask);
            if (hits.Any())
            {
                OnHit(CalculateNearestHit(hits));
                _lineRenderer.material.color = _detectionColor;
            }
            else
            {
                OnHit(null);
                _lineRenderer.material.color = _defaultColor;
            }
        }

        private RaycastHit CalculateNearestHit(RaycastHit[] hits)
        {
            float range = float.MaxValue;
            RaycastHit result = default;
            foreach (var item in hits)
            {
                if (item.distance < range)
                {
                    result = item;
                    range = item.distance;
                }
            }

            return result;
        }

        private void OnHit(RaycastHit? hit)
        {
            if (hit.HasValue)
            {
                _currentHit = hit.Value;
            }

            var hoverObjects = hit?.collider.GetComponents<InteractHoverObject>();
            var selectionObjects = hit?.collider.GetComponents<InteractSelectionObject>();
            SetHoverObjects(hoverObjects);
            SetSelectionObjects(selectionObjects);
        }

        public InteractArgs GetInteractArgs()
        {
            return new InteractArgs
            {
                OriginPosition = _lastRay.origin,
                OriginLookDirection = _lastRay.direction,
                HitPosition = _currentHit.point
            };
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

        public void InitRay()
        {
            _lineRenderer.enabled = true;
            OnActiveChanged.Invoke(true);
        }

        public void ClearRay()
        {
            _lineRenderer.enabled = false;
            foreach (var selection in _currentSelectionObjects)
            {
                selection.OnEndSelection(new InteractArgs());
            }
            OnHit(null);
            OnActiveChanged.Invoke(false);
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
    }
}