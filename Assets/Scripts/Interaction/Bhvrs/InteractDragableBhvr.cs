using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Interaction.Handlers
{
    public class InteractDragableBhvr : MonoBehaviour, InteractSelectionObject
    {
        [SerializeField]
        private Transform _root = default;
        private float _offset = 0.0f;

        public void OnBeginSelection(InteractArgs args)
        {
            _offset = Vector3.Distance(args.OriginPosition, _root.position);
        }

        public void OnEndSelection(InteractArgs args)
        {
        }

        public void OnUpdateSelection(InteractArgs args)
        {
            _root.position = args.OriginPosition + args.OriginLookDirection * _offset;
        }
    }
}
