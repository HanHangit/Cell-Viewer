using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorPrefHelper
{
    public class EditorPrefString : AEditorPrefValue<string>
    {
        public EditorPrefString(string key, bool projectSpecific = false)
            : base(key, projectSpecific)
        {
        }

        public override void LoadValue()
        {
            _value = EditorPrefs.GetString(GetKey());
        }

        protected override void OnValueSet(string value)
        {
            EditorPrefs.SetString(GetKey(), value);
        }
    }
}