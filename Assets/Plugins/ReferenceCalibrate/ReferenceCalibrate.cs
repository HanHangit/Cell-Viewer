using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary> Given a reference point in 3d space, this will apply transformations to have that reference point at the origin. Therefore the tracked object MUST be in the 'real' origin. </summary>
public class ReferenceCalibrate : MonoBehaviour
{
    [SerializeField]
    public string _fileName = "registration.json";

    [SerializeField]
    private Transform _referenceOrigin = null;

    [SerializeField]
    private Transform _rotation = null;
    [SerializeField]
    private Transform _translation = null;
    [SerializeField]
    private Vector3 _rotationOffset = new Vector3(0.0f, 0.0f, 180.0f);

    [SerializeField]
    private bool _enableLogging = true;
    [SerializeField]
    private bool _loadFileOnStart = true;

    private void Start()
    {
        if (_loadFileOnStart)
            ApplyRegistration(LoadFromFile(_fileName));
    }

    public void ApplyCurrentRegistration()
    {
        ApplyRegistration(GetCurrentRegistration());
    }

    public void SaveCurrentRegistrationToFile()
    {
        SaveRegistrationToFile(GetCurrentRegistration());
    }

    public void ApplyRegistrationFromFile()
    {
        ApplyRegistration(LoadFromFile(_fileName));
    }

    private Registration GetCurrentRegistration()
    {
        return new Registration(-_referenceOrigin.localPosition, Quaternion.Inverse(_referenceOrigin.localRotation));
    }

    private Registration LoadFromFile(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning($"Could not find Registration at path '{ path }', { gameObject.name }", gameObject);
            return new Registration(Vector3.zero, Quaternion.identity);
        }

        return JsonUtility.FromJson<Registration>(File.ReadAllText(path));
    }

    private void SaveRegistrationToFile(Registration r)
    {
        var jsonString = JsonUtility.ToJson(r, false);
        File.WriteAllText(_fileName, jsonString);

        if (_enableLogging)
        {

            Debug.Log($"Saved Registration { typeof(Registration).Name } to { _fileName }, { gameObject.name }", gameObject);
        }
    }

    private void ApplyRegistration(Registration r)
    {
        if (_enableLogging)
        {   
            Debug.Log($"Apply Registration: { r }, { gameObject.name }", gameObject);
        }

        _translation.localPosition = r.pos;
        _rotation.localRotation = r.rotation;
        _rotation.Rotate(_rotationOffset, Space.World);
    }

    [System.Serializable]
    public class Registration
    {
        public Vector3 pos;
        public Quaternion rotation;

        public Registration(Vector3 pos, Quaternion rotation)
        {
            this.pos = pos;
            this.rotation = rotation;
        }

        public override string ToString()
        {
            return "Pos: " + pos + ", Rotation: " + rotation;
        }
    }
}

