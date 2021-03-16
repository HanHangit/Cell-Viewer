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

        [SerializeField]
        private Camera _camera = default;

        private class MovementUnityEvent : UnityEvent<Vector3>
        {

        }

        private class QuaternionUnityEvent : UnityEvent<Quaternion>
        {

        }

        private void Update()
        {
            if (HOVR_Input.IsButtonClicked(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                InteractionPressedEvent?.Invoke();
            }
            if (HOVR_Input.IsButtonReleased(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                InteractionReleasedEvent?.Invoke();
            }

            MovementEvent?.Invoke(GetTrackPadMovement());
        }

        private Camera GetCurrentCamera()
        {
            return _camera;
        }

        private Vector3 GetTrackPadMovement()
        {
            Vector2 trackPadAxis = HOVR_Input.GetTrackpadAxis();
            Vector3 movement = Vector3.zero;
            const float MARGIN = 0.1f;

            if (trackPadAxis.x > MARGIN)
            {
                movement += GetCurrentCamera().transform.right * trackPadAxis.x;
            }
            if (trackPadAxis.x < MARGIN)
            {
                movement += GetCurrentCamera().transform.right * trackPadAxis.x;
            }
            if (trackPadAxis.y > MARGIN)
            {
                movement += GetCurrentCamera().transform.up * trackPadAxis.y;
            }
            if (trackPadAxis.y < MARGIN)
            {
                movement += GetCurrentCamera().transform.up * trackPadAxis.y;
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
