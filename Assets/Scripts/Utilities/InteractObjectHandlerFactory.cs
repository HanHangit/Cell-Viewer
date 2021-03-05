using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Interaction;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public abstract class InteractObjectHandlerFactory : MonoBehaviour
    {
        public abstract InteractObjectHandler CreateInteractObjectHandler();
    }
}
