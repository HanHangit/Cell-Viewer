using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorRef", menuName = "GUI/ColorRef")]
public class GUI_ColorRef : ScriptableObject
{
	[SerializeField]
	private Color _colorRef = default;

	public Color ColorRef => _colorRef;
}
