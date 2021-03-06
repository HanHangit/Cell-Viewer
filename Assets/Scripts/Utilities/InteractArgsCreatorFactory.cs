﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Interaction;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public abstract class InteractArgsCreatorFactory : MonoBehaviour
    {
        public abstract InteractArgsCreator GetInteractArgsCreator();
    }
}
