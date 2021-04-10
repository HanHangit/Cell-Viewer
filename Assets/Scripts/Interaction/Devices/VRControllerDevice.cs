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
        private HOVR_Tracker _tracker = default;

        private enum MovementMode
        {
            Up,
            Forward,
            Rotation
        }

        private class MovementUnityEvent : UnityEvent<Vector3>
        {

        }

        private class QuaternionUnityEvent : UnityEvent<Quaternion>
        {

        }

        private void Update()
        {
            if (HOVR_Input.IsButtonClicked(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger, (uint)_tracker.DeviceId))
            {
                InteractionPressedEvent?.Invoke();
            }
            if (HOVR_Input.IsButtonReleased(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger, (uint)_tracker.DeviceId))
            {
                InteractionReleasedEvent?.Invoke();
            }
            if (HOVR_Input.IsButtonClicked(Valve.VR.EVRButtonId.k_EButton_Grip, (uint)_tracker.DeviceId))
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

        private Quaternion GetTrackRotation()
        {
            Vector2 trackPadAxis = HOVR_Input.GetTrackpadAxis((uint)_tracker.DeviceId);
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
            Vector2 trackPadAxis = HOVR_Input.GetTrackpadAxis((uint)_tracker.DeviceId);
            return trackPadAxis.normalized * 0.005f;
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

        public int GetIndex()
        {
            return _tracker.DeviceId;
        }
    }
}
