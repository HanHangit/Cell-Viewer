using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUI_SetColorRefToText : MonoBehaviour
{
	[SerializeField]
	private GUI_ColorRef _colorRef = default;
	[SerializeField]
	private TMPro.TextMeshProUGUI _text = default;

	private void Awake()
	{
		if (_text == null)
		{
			_text = GetComponent<TextMeshProUGUI>();
		}

		if (_colorRef != null && _text)
		{
			_text.color = _colorRef.ColorRef;
		}
		else
		{
			Debug.LogWarning("No color or text assignet.", this.gameObject);
		}
	}
}
