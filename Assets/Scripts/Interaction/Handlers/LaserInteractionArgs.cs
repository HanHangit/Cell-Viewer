using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Interaction.Handlers
{
    public class LaserInteractionArgs : InteractArgsCreatorFactory
    {
        [SerializeField]
        private LaserInteractionObjectHandler _laserInteractionObjectHandler = default;

        public override InteractArgsCreator GetInteractArgsCreator()
        {
            return _laserInteractionObjectHandler;
        }
    }
}
