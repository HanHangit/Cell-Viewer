using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utilities;
using HeadlessOpenVR;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Interaction.Devices
{
    public class VRControllerDevice : InputInteractionHandlerFactory, InputInteractionHandler
    {
        private UnityEvent InteractionPressedEvent = new UnityEvent();
        private UnityEvent InteractionReleasedEvent = new UnityEvent();
        private UnityEvent<Vector3> MovementEvent = new MovementUnityEvent();
        private UnityEvent<Quaternion> RotationEvent = new QuaternionUnityEvent();
        private MovementMode _movementMode = MovementMode.Up;

        [SerializeField]
        private Transform _offsetTransform = default;
        [SerializeField]
        private Transform _offsetRotation = default;

        private enum MovementMode
        {
            Up,
            Forward,
            Rotation
        }

        [SerializeField]
        private Camera _camera = default;

        private Vector3 _camStartPos = Vector3.zero;
        private Vector3 _offsetStartPos = Vector3.zero;
        private Quaternion _startQuaternion = Quaternion.identity;
        private Quaternion _offsetStartQuaternion = Quaternion.identity;

        private class MovementUnityEvent : UnityEvent<Vector3>
        {

        }

        private class QuaternionUnityEvent : UnityEvent<Quaternion>
        {

        }

        private void Start()
        {
            _camStartPos = _camera.transform.position;
            _offsetStartPos = _offsetTransform.position;
            _startQuaternion = _camera.transform.rotation;
            _offsetStartQuaternion = _offsetRotation.rotation;
        }

        private void Update()
        {
            _offsetTransform.position = _offsetStartPos + (_camera.transform.position - _camStartPos);
            _offsetRotation.rotation = Quaternion.Euler(_offsetStartQuaternion.eulerAngles.x + (_camera.transform.rotation.eulerAngles.x - _startQuaternion.eulerAngles.x),
                _offsetStartQuaternion.eulerAngles.y + (_camera.transform.rotation.eulerAngles.y - _startQuaternion.eulerAngles.y),
                _offsetStartQuaternion.eulerAngles.z + (_camera.transform.rotation.eulerAngles.z - _startQuaternion.eulerAngles.z));

            if (HOVR_Input.IsButtonClicked(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                InteractionPressedEvent?.Invoke();
            }
            if (HOVR_Input.IsButtonReleased(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                InteractionReleasedEvent?.Invoke();
            }
            if (HOVR_Input.IsButtonClicked(Valve.VR.EVRButtonId.k_EButton_Grip))
            {
                if (_movementMode == MovementMode.Rotation)
                {
                    _movementMode = MovementMode.Up;
                }
                else
                {
                    _movementMode += 1;
                }
            }

            MovementEvent?.Invoke(GetTrackPadMovement());
            RotationEvent?.Invoke(GetTrackRotation());
        }

        private Camera GetCurrentCamera()
        {
            return _camera;
        }

        private Quaternion GetTrackRotation()
        {
            Vector2 trackPadAxis = HOVR_Input.GetTrackpadAxis();
            Quaternion rotation = Quaternion.identity;
            const float MARGIN = 0.1f;

            if (_movementMode == MovementMode.Rotation)
            {
                if (trackPadAxis.x > MARGIN || trackPadAxis.x < MARGIN)
                {
                    rotation *= Quaternion.AngleAxis(trackPadAxis.x, Vector3.up);
                }
                if (trackPadAxis.y > MARGIN || trackPadAxis.y < MARGIN)
                {
                    rotation *= Quaternion.AngleAxis(trackPadAxis.y * -1, Vector3.right);
                }
            }

            return rotation;
        }

        private Vector3 GetTrackPadMovement()
        {
            Vector2 trackPadAxis = HOVR_Input.GetTrackpadAxis();
            Vector3 movement = Vector3.zero;
            const float MARGIN = 0.1f;

            if (_movementMode != MovementMode.Rotation)
            {
                if (trackPadAxis.x > MARGIN)
                {
                    movement += GetCurrentCamera().transform.right * trackPadAxis.x;
                }
                if (trackPadAxis.x < MARGIN)
                {
                    movement += GetCurrentCamera().transform.right * trackPadAxis.x;
                }
                if (trackPadAxis.y > MARGIN || trackPadAxis.y < MARGIN)
                {
                    if (_movementMode == MovementMode.Forward)
                    {
                        movement += GetCurrentCamera().transform.forward * trackPadAxis.y;
                    }
                    else
                    {
                        movement += GetCurrentCamera().transform.up * trackPadAxis.y;
                    }
                }
            }

            return movement * 0.005f;
        }

        public override InputInteractionHandler GetInteractionHandler()
        {
            return this;
        }

        public void AddInteractionPressedEventListener(Action callback)
        {
            InteractionPressedEvent.AddListener(() => callback());
        }

        public void AddInteractionReleasedEventListener(Action callback)
        {
            InteractionReleasedEvent.AddListener(() => callback());
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
