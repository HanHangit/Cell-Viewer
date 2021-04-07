using Assets.Scripts.Interaction;
using Assets.Scripts.Interaction.Bhvrs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCubeToWorldRay : MonoBehaviour, InteractSelectionObject, InteractHoverObject
{
    [SerializeField]
    private MeshRenderer _meshRenderer = default;
    [SerializeField]
    private CameraRayCreator _cameraRayCreator = default;
    [SerializeField]
    private bool _invertMinPoint = false;

    public void OnBeginSelection(InteractArgs args)
    {
    }

    public void OnEndSelection(InteractArgs args)
    {
    }

    public void OnUpdateSelection(InteractArgs args)
    {
    }

    private void CalculateRaycastHits(RaycastHit[] hits)
    {

    }

    private void ClearRaycastHits()
    {
        _cameraRayCreator.ClearRay();
    }

    public void OnHoverBegin(InteractArgs args)
    {
        _cameraRayCreator.InitRay();
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
        Debug.Log(resultPoint);
        return resultPoint;
    }

    public void OnHoverEnd(InteractArgs args)
    {
        ClearRaycastHits();
    }

    public void OnHoverUpdate(InteractArgs args)
    {
        _cameraRayCreator.CreateRaycast(GetMeshContactScreenPoint(args.HitPosition));
    }
}
