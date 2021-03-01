using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Interaction
{
    public abstract class InteractBhvr : MonoBehaviour, InteractObject
    {
        public virtual void BeginTrigger(InteractArgs args)
        {

        }

        public virtual void EndTrigger(InteractArgs args)
        {
        }

        public virtual void HoverBegin(InteractArgs args)
        {

        }

        public virtual void HoverUpdate(InteractArgs args)
        {

        }

        public virtual void HoverEnd(InteractArgs args)
        {

        }
    }
}
