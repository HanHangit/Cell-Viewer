using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Interaction;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Movement
{
    public class MouseKeyboardDebugMovement : InputInteractionHandlerFactory, InputInteractionHandler
    {
        private UnityEvent InteractionPressedEvent = new UnityEvent();
        private UnityEvent InteractionReleasedEvent = new UnityEvent();
        private UnityEvent<Vector3> MovementEvent = new MovementUnityEvent();
        private UnityEvent<Quaternion> RotationEvent = new QuaternionUnityEvent();

        private class MovementUnityEvent : UnityEvent<Vector3>
        {

        }

        private class QuaternionUnityEvent : UnityEvent<Quaternion>
        {

        }

        [SerializeField]
        private Transform _deviceRoot = default;
        [SerializeField]
        private Camera _camera = default;
        private Vector2 _oldMousePosition = default;

        private void Update()
        {
            CheckMousePosition();
            UpdateInteractionEvents();
            UpdateMouseMovementEvents();
            UpdateMouseScrollEvents();

            _oldMousePosition = Input.mousePosition;
        }

        private void UpdateInteractionEvents()
        {
            if (Input.GetMouseButtonDown(0))
            {
                InteractionPressedEvent?.Invoke();
            }
            if (Input.GetMouseButtonUp(0))
            {
                InteractionReleasedEvent?.Invoke();
            }
        }

        private void UpdateMouseMovementEvents()
        {
            var keyboardMovement = CalculateKeyboardMovement();
            var rotationMovement = CalculateKeyboardRotation();

            RotationEvent?.Invoke(rotationMovement);


            MovementEvent?.Invoke(keyboardMovement);
        }

        private Quaternion CalculateKeyboardRotation()
        {
            var result = Quaternion.identity;
            if (Input.GetKey(KeyCode.E))
            {
                result *= Quaternion.AngleAxis(20.0f * Time.deltaTime, _camera.transform.up);
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                result *= Quaternion.AngleAxis(-20.0f * Time.deltaTime, _camera.transform.up);
            }

            return result;
        }

        private Vector3 CalculateKeyboardMovement()
        {
            Vector3 result = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                result += _camera.transform.up;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                result += _camera.transform.right * -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                result += _camera.transform.right;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                result += _camera.transform.up * -1;
            }

            return result.normalized * 0.01f;
        }

        private void UpdateMouseScrollEvents()
        {
            var scrollDelta = Input.mouseScrollDelta;

            if (scrollDelta != Vector2.zero)
            {
                MovementEvent?.Invoke(_deviceRoot.forward * scrollDelta.y * 0.1f);
            }
        }

        private Vector3 CalculateWorldMovement()
        {
            var newMousePosition = Input.mousePosition;
            var moveDelta = GetMouseWorldPos(_oldMousePosition) - GetMouseWorldPos(newMousePosition);

            return moveDelta;
        }

        private Quaternion CalculateRotation()
        {
            var moveDelta = _oldMousePosition - new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            return Quaternion.identity;
        }

        private Vector3 GetMouseWorldPos(Vector3 mouseScreenPosition)
        {
            var mouseRay = _camera.ScreenPointToRay(mouseScreenPosition);
            return mouseRay.origin + mouseRay.direction * _camera.nearClipPlane;
        }

        private void CheckMousePosition()
        {
            var mouseWorldPos = GetMouseWorldPos(Input.mousePosition);

            _deviceRoot.LookAt(mouseWorldPos);
            _deviceRoot.transform.position = _camera.transform.position;
        }

        public override InputInteractionHandler GetInteractionHandler()
        {
            return this;
        }

        public void AddInteractionPressedEventListener(UnityAction callback)
        {
            InteractionPressedEvent.AddListener(callback);
        }

        public void AddInteractionReleasedEventListener(UnityAction callback)
        {
            InteractionReleasedEvent.AddListener(callback);
        }

        public void AddMovementEventListener(UnityAction<Vector3> callback)
        {
            MovementEvent.AddListener(callback);
        }

        public void AddRotationEventListener(UnityAction<Quaternion> callback)
        {
            RotationEvent.AddListener(callback);
        }
    }
}
