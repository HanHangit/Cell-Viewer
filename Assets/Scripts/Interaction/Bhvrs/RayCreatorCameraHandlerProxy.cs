using Assets.Scripts.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Interaction.Bhvrs
{
    public class RayCreatorCameraHandlerProxy : CameraHandlerFactory, CameraHandler
    {
        [SerializeField]
        private CameraRayCreator _rayCreator = default;
        [SerializeField]
        private CameraHandlerFactory _cameraFactory = default;

        private CameraHandler _cameraHandler = default;

        private bool _isActive = false;

        private void Start()
        {
            _cameraHandler = _cameraFactory.CreateCameraHandler();
            _rayCreator.OnActiveChanged.AddListener(RayActiveChangedListener);
        }

        private void RayActiveChangedListener(bool state)
        {
            _isActive = state;
        }

        public void MoveCamera(Vector3 move)
        {
            if (_isActive)
            {
                _cameraHandler.MoveCamera(move);
            }
        }

        public void RotateCamera(Quaternion rotation)
        {
            if (_isActive)
            {
                _cameraHandler.RotateCamera(rotation);
            }
        }

        public override CameraHandler CreateCameraHandler()
        {
            return this;
        }

        public Vector3 GetForwardVector()
        {
            return _cameraHandler.GetForwardVector();
        }
    }
}