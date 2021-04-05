using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interaction
{
    public class InteractHighlightObjectBhvr : MonoBehaviour, InteractHoverObject, InteractSelectionObject
    {
        [SerializeField]
        private MeshRenderer _meshRenderer = default;
        [SerializeField]
        private Color _hoverColor = Color.red;
        [SerializeField]
        private Color _selectionColor = Color.magenta;
        private Color _oldColor = Color.black;

        private bool _isSelected = false;
        private bool _isHovered = false;

        private void Awake()
        {
            _oldColor = _meshRenderer.material.color;
        }

        private void OnValidate()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void OnHoverBegin(InteractArgs args)
        {
            _isHovered = true;
            TrySetHoverColor();
        }

        private void TrySetHoverColor()
        {
            if (!_isSelected)
            {
                _meshRenderer.material.color = _hoverColor;
            }
        }

        private void TrySetSelectionColor()
        {
            _meshRenderer.material.color = _selectionColor;
        }

        private void ResetColor()
        {
            if (!_isSelected)
            {
                if (_isHovered)
                {
                    _meshRenderer.material.color = _hoverColor;
                }
                else
                {
                    _meshRenderer.material.color = _oldColor;
                }
            }
        }

        public void OnHoverUpdate(InteractArgs args)
        {
        }

        public void OnHoverEnd(InteractArgs args)
        {
            _isHovered = false;
            ResetColor();
        }

        public void OnBeginSelection(InteractArgs args)
        {
            _isSelected = true;
            TrySetSelectionColor();
        }

        public void OnEndSelection(InteractArgs args)
        {
            _isSelected = false;
            ResetColor();
        }

        public void OnUpdateSelection(InteractArgs args)
        {

        }
    }
}
