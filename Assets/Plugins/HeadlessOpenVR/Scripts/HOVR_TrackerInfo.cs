using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace HeadlessOpenVR
{
    public class HOVR_TrackerInfo
    {
        private int _deviceId = -1;
        public int DeviceId { get { return _deviceId; } }

        public string SerialNumber = "";
        public string AdditionalString;
        public bool HasBattery = false;

        public ETrackedDeviceClass DeviceClass;

        public HOVR_TrackerInfo(int deviceId, ETrackedDeviceClass deviceClass)
        {
            _deviceId = deviceId;
            DeviceClass = deviceClass;
        }

        public override string ToString()
        {
            return $"SN: '{ SerialNumber }'; DeviceID: '{ _deviceId }', { AdditionalString }. DeviceClass: { DeviceClass }";
        }
    }
}