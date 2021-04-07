using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Interaction
{
    public abstract class InteractRayCreator : MonoBehaviour
    {
        public abstract RaycastHit[] GetRaycastHits();
    }
}