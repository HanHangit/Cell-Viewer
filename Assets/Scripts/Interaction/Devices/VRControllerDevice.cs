using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utilities;
using HeadlessOpenVR;
using UnityEngine.Events;

namespace Assets.Scripts.Interaction.Devices
{
    public class VRControllerDevice : InputInteractionHandlerFactory, InputInteractionHandler
    {
        public UnityEvent InteractionPressedEvent = new UnityEvent();
        public UnityEvent InteractionReleasedEvent = new UnityEvent();

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
