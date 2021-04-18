using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectOnKeyInput : MonoBehaviour
{
	[SerializeField]
	private List<Element> _objects = new List<Element>();

	private void Update()
	{
		foreach (var item in _objects)
		{
			if (Input.GetKeyDown(item.KeyCode))
			{
				item.ToggleObj();
			}
		}
	}

	[System.Serializable]
	class Element
	{
		public GameObject Object;
		public KeyCode KeyCode;

		public void ToggleObj()
		{
			if (Object.activeInHierarchy)
			{
				Object.SetActive(false);
			}
			else
			{
				Object.SetActive(true);
			}
		}
		
	}
}
