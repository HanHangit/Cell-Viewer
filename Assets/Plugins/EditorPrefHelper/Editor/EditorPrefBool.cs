using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorPrefHelper
{
    public class EditorPrefBool : AEditorPrefValue<bool>
    {

        public EditorPrefBool(string key, bool projectSpecific = false)
            : base(key, projectSpecific)
        {
        }

        public override void LoadValue()
        {
            _value = EditorPrefs.GetBool(GetKey());
        }

        protected override void OnValueSet(bool value)
        {
            EditorPrefs.SetBool(GetKey(), value);
        }
    }
}