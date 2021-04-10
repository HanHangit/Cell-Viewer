using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Interaction
{
    public struct InteractArgs
    {
        public Vector3 OriginPosition { get; set; }
        public Vector3 OriginLookDirection { get; set; }
        public Vector3 Offset { get; set; }
        public Vector3 HitPosition { get; set; }
        public int ControllerIndex { get; set; }
        public InputInteractionHandlerFactory InputFactory { get; set; }
    }
}
