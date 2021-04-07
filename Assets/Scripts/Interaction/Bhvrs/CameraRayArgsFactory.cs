using Assets.Scripts.Utilities;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Interaction.Bhvrs
{
    public class CameraRayArgsFactory : InteractArgsCreatorFactory
    {
        [SerializeField]
        private CameraRayCreator _cameraCreator = default;

        public override InteractArgsCreator GetInteractArgsCreator()
        {
            return _cameraCreator;
        }
    }
}