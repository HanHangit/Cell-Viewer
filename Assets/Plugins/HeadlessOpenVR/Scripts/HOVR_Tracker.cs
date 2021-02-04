using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Valve.VR;

namespace HeadlessOpenVR
{
    public class HOVR_Tracker : MonoBehaviour
    {   
        [System.Serializable]
        public class TrackerEvent : UnityEvent<HOVR_Tracker> { }

        public const int UNINIT_DEVICE_ID = -1;
#if UNITY_EDITOR
        [TextArea(1, 4)]
        public string DebugText = "";
#endif

        [SerializeField]
        private Transform _pivot = null;

        [SerializeField]
        private bool _isController = false;
        public bool IsController { get { return _isController; } }

        [SerializeField]
        private string _serialNumber = "";
        public string SerialNumber { get { return _serialNumber; } }

        [SerializeField, HOVR_ReadOnly]
        private Valve.VR.ETrackingResult _trackingState = ETrackingResult.Uninitialized;
        public Valve.VR.ETrackingResult GetTrackingState() { return _trackingState; }

        [HideInInspector]
        public TrackerEvent OnTrackerStateChangedEvent = new TrackerEvent();

        private int _deviceId = -1;
        public int DeviceId { get { return _deviceId; } }

        private bool _hasBattery = false;
        public bool HasBattery { get { return _hasBattery; } }

        private float _batteryPercent = 1.0f;
        public float BatteryPercent { get { return _batteryPercent; } }

        private bool _isCharging = false;
        public bool IsCharging { get { return _isCharging; } }

        public enum ESwizzle { XYZ, XZY, YXZ, YZX, ZXY, ZYX }
        
        [SerializeField]
        private ESwizzle _swizzle = default;

        private void Reset()
        {
            if (!_pivot)
                _pivot = transform;
        }

        public virtual void Init(HOVR_HeadlessOpenVR openVR, HOVR_TrackerInfo info)
        {
            _deviceId = info.DeviceId;
            _hasBattery = info.HasBattery;
        }

        public virtual void ReceivePose(Vector3 pos, Quaternion rotation)
        {
            _pivot.localPosition = pos;

            var inRotation = rotation.eulerAngles;
            _pivot.localRotation = Quaternion.Euler(Swizzle(inRotation, _swizzle));
        }

        /// <summary> Forces <see cref="OnTrackerStateChangedEvent"/> to be fired. </summary>
        public void BroadcastTrackingState()
        {
            SetTrackingState(_trackingState, _batteryPercent, true);
        }

        public void SetTrackingState(Valve.VR.ETrackingResult state, float batteryPercent, bool isCharging, bool force = false)
        {
            bool changed = false;

            //only update if the battery changed enough:
            const float MIN_BATTERY_CHANGE_PERCENT = 0.01f;
            if (Mathf.Abs(_batteryPercent - batteryPercent) > MIN_BATTERY_CHANGE_PERCENT)
            {
                _batteryPercent = batteryPercent;
                changed = true;
            }

            if (_trackingState != state)
            {
                _trackingState = state;
                changed = true;
            }

            if(isCharging != _isCharging)
            {
                _isCharging = isCharging;
                changed = true;
            }

            if (force || changed)
                OnTrackerStateChangedEvent.Invoke(this);
        }

        private Vector3 Swizzle(Vector3 v, ESwizzle s)
        {
            switch (s)
            {
                case ESwizzle.XYZ:
                    return v;
                case ESwizzle.XZY:
                    return new Vector3(v.x, v.z, v.y);
                case ESwizzle.YXZ:
                    return new Vector3(v.y, v.x, v.z);
                case ESwizzle.YZX:
                    return new Vector3(v.y, v.z, v.x);
                case ESwizzle.ZXY:
                    return new Vector3(v.z, v.x, v.y);
                case ESwizzle.ZYX:
                    return new Vector3(v.z, v.y, v.x);
                default:
                    return v;
            }
        }
    }
}