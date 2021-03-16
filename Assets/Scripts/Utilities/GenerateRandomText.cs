using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomText
{
	private List<string> _txt = new List<string>();
	public GenerateRandomText(List<string> txt)
	{
		_txt = txt;
	}

	public string GetRandomText()
	{
		if (_txt.Count == 0)
		{
			return "Empty_Text";
		}
		else
		{
			var index = UnityEngine.Random.Range(0, _txt.Count - 1);
			var element = _txt[index];
			_txt.Remove(element);
			return element;
		}
	}
}

