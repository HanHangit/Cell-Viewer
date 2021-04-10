using Assets.Scripts.Interaction;
using Assets.Scripts.Interaction.Bhvrs;
using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCubeToWorldRay : MonoBehaviour, InteractHoverObject
{
    [SerializeField]
    private MeshRenderer _meshRenderer = default;
    private Dictionary<int, CameraRayCreator> _cameraRayCreatorMapping = new Dictionary<int, CameraRayCreator>();
    [SerializeField]
    private CameraRayCreator _rayCreatorPrefab = default;
    [SerializeField]
    private bool _invertMinPoint = false;

    private CameraRayCreator GetCameraRayCreator(InteractArgs args)
    {
        if (!_cameraRayCreatorMapping.ContainsKey(args.ControllerIndex))
        {
            var newRayCreator = Instantiate(_rayCreatorPrefab, _rayCreatorPrefab.transform.parent);
            var interactHandler = newRayCreator.GetComponent<InteractionHandler>();
            interactHandler.SetInputHandlerFactory(args.InputFactory);
            _cameraRayCreatorMapping.Add(args.ControllerIndex, newRayCreator);
        }
        return _cameraRayCreatorMapping[args.ControllerIndex];
    }

    private void ClearRaycastHits(InteractArgs args)
    {
        GetCameraRayCreator(args).ClearRay();
    }

    public void OnHoverBegin(InteractArgs args)
    {
        GetCameraRayCreator(args).InitRay();
    }

    private Vector3 CalculateMinPoint(Vector3 contactPoint)
    {
        var screenPoint = contactPoint - _meshRenderer.bounds.min;
        var min = _meshRenderer.bounds.min;

        if (_invertMinPoint)
        {
            if (Mathf.Abs(screenPoint.x) < 0.001f)
            {
                return new Vector3(min.x, min.y, _meshRenderer.bounds.max.z);
            }
        }

        return min;
    }

    private Vector2 GetMeshContactScreenPoint(Vector3 contactPoint)
    {
        var resultPoint = Vector2.zero;
        var minPoint = CalculateMinPoint(contactPoint);
        var screenPoint = contactPoint - minPoint;

        var size = _meshRenderer.bounds.size;

        if (Mathf.Abs(screenPoint.x) < 0.001f)
        {
            resultPoint = new Vector2(screenPoint.z / size.z, screenPoint.y / size.y);
        }

        if (Mathf.Abs(screenPoint.y) < 0.001f)
        {
            resultPoint = new Vector2(screenPoint.z / size.z, screenPoint.x / size.x);
        }

        if (Mathf.Abs(screenPoint.z) < 0.001f)
        {
            resultPoint = new Vector2(screenPoint.x / size.x, screenPoint.y / size.y);
        }

        resultPoint = new Vector2(Mathf.Abs(resultPoint.x), Mathf.Abs(resultPoint.y));
        return resultPoint;
    }

    public void OnHoverEnd(InteractArgs args)
    {
        ClearRaycastHits(args);
    }

    public void OnHoverUpdate(InteractArgs args)
    {
        GetCameraRayCreator(args).CreateRaycast(GetMeshContactScreenPoint(args.HitPosition));
    }
}
