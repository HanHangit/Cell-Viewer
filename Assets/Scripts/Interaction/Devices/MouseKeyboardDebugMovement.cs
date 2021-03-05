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
        public UnityEvent InteractionPressedEvent = new UnityEvent();
        public UnityEvent InteractionReleasedEvent = new UnityEvent();


        [SerializeField]
        private Transform _deviceRoot = default;
        [SerializeField]
        private Camera _camera = default;

        private void Update()
        {
            CheckMousePosition();

            if (Input.GetMouseButtonDown(0))
            {
                InteractionPressedEvent?.Invoke();
            }
            if (Input.GetMouseButtonUp(0))
            {
                InteractionReleasedEvent?.Invoke();
            }
        }

        private void CheckMousePosition()
        {
            var mouseRay = _camera.ScreenPointToRay(Input.mousePosition);

            var mouseWorldPos = mouseRay.origin + mouseRay.direction * _camera.nearClipPlane;

            _deviceRoot.LookAt(mouseWorldPos);
            _deviceRoot.transform.position = _camera.transform.position;
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
    }
}
