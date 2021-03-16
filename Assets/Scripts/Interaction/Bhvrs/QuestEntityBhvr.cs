using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interaction.Bhvrs
{
    public class QuestEntityBhvr : MonoBehaviour
    {
        [SerializeField]
        private Entity _entity;

        public Entity Entity => _entity;

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}
