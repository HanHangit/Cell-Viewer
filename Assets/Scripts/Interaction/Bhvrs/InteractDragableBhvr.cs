using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Interaction.Handlers
{
    public class InteractDragableBhvr : InteractBhvr
    {
        [SerializeField]
        private Transform _root = default;
        private bool _isHovered = false;
        private bool _isSelected = false;
        private float _offset = 0.0f;

        public override void HoverBegin(InteractArgs args)
        {
            base.HoverBegin(args);
            _isHovered = true;
        }

        public override void HoverEnd(InteractArgs args)
        {
            base.HoverEnd(args);
            _isHovered = false;
            _isSelected = false;
        }

        public override void HoverUpdate(InteractArgs args)
        {
            base.HoverUpdate(args);

            if (_isSelected)
            {
                _root.position = args.OriginPosition + args.OriginLookDirection * _offset;
            }
        }

        public override void BeginTrigger(InteractArgs args)
        {
            base.BeginTrigger(args);

            if (_isHovered)
            {
                _isSelected = true;
                _offset = Vector3.Distance(args.OriginPosition, _root.position);
            }
        }

        public override void EndTrigger(InteractArgs args)
        {
            base.EndTrigger(args);

            _isSelected = false;
        }
    }
}
