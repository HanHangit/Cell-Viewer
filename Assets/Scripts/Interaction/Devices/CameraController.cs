﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Interaction.Devices
{
    public class CameraController : CameraHandlerFactory, CameraHandler
    {
        [SerializeField]
        private Transform _transform = default;

        public void MoveCamera(Vector3 move)
        {
            _transform.position += move;
        }

        public void RotateCamera(Quaternion rotation)
        {
            _transform.Rotate(rotation.eulerAngles);
        }

        public override CameraHandler CreateCameraHandler()
        {
            return this;
        }
    }
}