using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorPrefHelper
{
    public class EditorPrefInt : AEditorPrefValue<int>
    {
        public EditorPrefInt(string key, bool projectSpecific = false)
            : base(key, projectSpecific)
        {
        }

        public override void LoadValue()
        {
            _value = EditorPrefs.GetInt(GetKey());
        }

        protected override void OnValueSet(int value)
        {
            EditorPrefs.SetInt(GetKey(), value);
        }
    }
}