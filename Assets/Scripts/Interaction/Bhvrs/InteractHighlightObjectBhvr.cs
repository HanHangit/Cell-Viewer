using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interaction
{
    public class InteractHighlightObjectBhvr : InteractBhvr
    {
        [SerializeField]
        private MeshRenderer _meshRenderer = default;
        [SerializeField]
        private Color _newColor = Color.red;
        private Color _oldColor = Color.black;

        public override void HoverBegin(InteractArgs args)
        {
            base.HoverBegin(args);

            _oldColor = _meshRenderer.material.color;
            _meshRenderer.material.color = _newColor;
        }

        public override void HoverEnd(InteractArgs args)
        {
            base.HoverEnd(args);

            _meshRenderer.material.color = _oldColor;
        }
    }
}
