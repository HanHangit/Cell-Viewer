using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorPrefHelper
{   
    public abstract class AEditorPrefValue<T>
    {
        private static string _projectName;

        protected string _baseKey;
        public string BaseKey { get { return _baseKey; } }

        protected T _value;

        private bool _isProjectSpecific;

        private string _finalKey;
        public string GetKey()
        {
            if (string.IsNullOrEmpty(_finalKey))
                _finalKey = BuildFinalKey();

            return _finalKey;
        }

        public AEditorPrefValue(string key, bool projectSpecific)
        {
            _baseKey = key;
            _isProjectSpecific = projectSpecific;
        }

        public abstract void LoadValue();

        protected abstract void OnValueSet(T value);

        public void SetValue(T t)
        {
            _value = t;
            OnValueSet(_value);
        }

        public T GetValue()
        {
            return _value;
        }

        private string BuildFinalKey()
        {
            if (_isProjectSpecific)
                return GetProjectName() + "." + _baseKey;

            return _baseKey;
        }

        public static string GetProjectName()
        {
            if (string.IsNullOrEmpty(_projectName))
            {
                string[] s = Application.dataPath.Split('/');
                _projectName = s[s.Length - 2];
            }

            return _projectName;
        }
    }
}