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

        private static Dictionary<uint, InputDevice> _inputDevices = new Dictionary<uint, InputDevice>();
        private static bool _displayedWarning = false;

        public static void Update(CVRSystem vrSystem, uint deviceId)
        {
            if (!_inputDevices.ContainsKey(deviceId))
            {
                _inputDevices.Add(deviceId, new InputDevice());
            }

            _inputDevices[deviceId].Update(vrSystem, deviceId);
        }

        public static bool IsButtonClicked(EVRButtonId buttonId, uint deviceID)
        {
            return GetInputDevice(deviceID)?.IsButtonClicked(buttonId) ?? false;
        }

        private static InputDevice GetInputDevice(uint deviceID)
        {
            if (!_inputDevices.ContainsKey(deviceID))
            {
                return null;
            }

            return _inputDevices[deviceID];
        }

        public static Vector2 GetTrackpadAxis(uint deviceID)
        {
            return GetInputDevice(deviceID)?.GetTrackpadAxis() ?? Vector2.zero;
        }

        public static bool IsButtonDown(EVRButtonId buttonId, uint deviceID)
        {
            return GetInputDevice(deviceID)?.IsButtonDown(buttonId) ?? false;
        }

        public static bool IsButtonReleased(EVRButtonId buttonId, uint deviceID)
        {
            return GetInputDevice(deviceID)?.IsButtonReleased(buttonId) ?? false;
        }

        private class InputDevice
        {
            private VRControllerState_t _oldState;
            private VRControllerState_t _curState;

            public void Update(CVRSystem vrSystem, uint deviceId)
            {
                _oldState = _curState;
                if (!vrSystem.GetControllerState(deviceId, ref _curState, SIZE_OF_VR_CONTROLLER_STATE_T))
                {
                    if (!_displayedWarning)
                    {
                        Debug.LogWarning("Controller State could not be read.");
                        _displayedWarning = true;
                    }
                }

                else if (_displayedWarning)
                {
                    _displayedWarning = false;
                }
            }

            public bool IsButtonClicked(EVRButtonId buttonId)
            {
                var wasPressed = ContainsButton(_oldState.ulButtonPressed, buttonId);
                var isPressed = ContainsButton(_curState.ulButtonPressed, buttonId);
                return !wasPressed && isPressed;
            }

            public Vector2 GetTrackpadAxis()
            {
                return new Vector2(_curState.rAxis0.x, _curState.rAxis0.y);
            }

            public bool IsButtonDown(EVRButtonId buttonId)
            {
                return ContainsButton(_curState.ulButtonPressed, buttonId);
            }

            public bool IsButtonReleased(EVRButtonId buttonId)
            {
                var wasPressed = ContainsButton(_oldState.ulButtonPressed, buttonId);
                var isPressed = ContainsButton(_curState.ulButtonPressed, buttonId);
                return wasPressed && !isPressed;
            }

            private bool ContainsButton(ulong buttonMask, EVRButtonId buttonId)
            {
                return (buttonMask & (1UL << ((int)buttonId))) != 0L;
            }
        }
    }
}