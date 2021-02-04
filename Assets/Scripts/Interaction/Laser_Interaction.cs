using HeadlessOpenVR;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Interaction
{
    public class Laser_Interaction : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer _lineRenderer = default;
        [SerializeField]
        private LayerMask _collisionMask = default;
        [SerializeField]
        private Color _detectionColor = Color.yellow;
        [SerializeField]
        private Color _dragColor = Color.green;
        [SerializeField]
        private Color _defaultColor = Color.red;
        [SerializeField]
        private Transform _rayOrigin = default;

        private Transform _dragObj = null;
        private float _offsetRange = 0.0f;

        [SerializeField]
        private float _dpadSpeed = 0.3f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            var ray = new Ray(_rayOrigin.position, _rayOrigin.forward);
            var hits = Physics.RaycastAll(ray, 1000, _collisionMask);
            if (_dragObj == null && hits.Any())
            {
                _lineRenderer.material.color = _detectionColor;

                if (HOVR_Input.IsButtonClicked(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
                {
                    _dragObj = hits[0].transform;
                    _offsetRange = Vector3.Distance(_rayOrigin.position, _dragObj.position);
                };
            }
            else if (_dragObj != null && HOVR_Input.IsButtonDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                _lineRenderer.material.color = _dragColor;
                _dragObj.transform.position = _rayOrigin.position + _rayOrigin.forward * _offsetRange;
            }
            else
            {
                _dragObj = null;
                _lineRenderer.material.color = _defaultColor;
            }
        }
    }
}