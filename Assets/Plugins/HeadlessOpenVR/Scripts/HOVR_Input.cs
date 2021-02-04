using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace HeadlessOpenVR
{   
    public class HOVR_Input
    {
        private static readonly uint SIZE_OF_VR_CONTROLLER_STATE_T = (uint)System.Runtime.InteropServices.Marshal.SizeOf<VRControllerState_t>();

        private static VRControllerState_t _oldState;
        private static VRControllerState_t _curState;

        private static bool _displayedWarning = false;

        public static void Update(CVRSystem vrSystem, uint deviceId)
        {
            _oldState = _curState;
            if (!vrSystem.GetControllerState(deviceId, ref _curState, SIZE_OF_VR_CONTROLLER_STATE_T))
            {
                if(!_displayedWarning)
                {
                    Debug.LogWarning("Controller State could not be read.");
                    _displayedWarning = true;
                }
            }

            else if(_displayedWarning)
            {
                _displayedWarning = false;
            }
        }

        public static bool IsButtonClicked(EVRButtonId buttonId)
        {   
            var wasPressed = ContainsButton(_oldState.ulButtonPressed, buttonId);
            var isPressed = ContainsButton(_curState.ulButtonPressed, buttonId);
            return !wasPressed && isPressed;
        }

        public static bool IsButtonDown(EVRButtonId buttonId)
        {
            return ContainsButton(_curState.ulButtonPressed, buttonId);
        }

        public static bool IsButtonReleased(EVRButtonId buttonId)
        {
            var wasPressed = ContainsButton(_oldState.ulButtonPressed, buttonId);
            var isPressed = ContainsButton(_curState.ulButtonPressed, buttonId);
            return wasPressed && !isPressed;
        }

        private static bool ContainsButton(ulong buttonMask, EVRButtonId buttonId)
        {
            return (buttonMask & (1UL << ((int)buttonId))) != 0L;
        }
    }
}