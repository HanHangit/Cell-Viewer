using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_FieldController : MonoBehaviour
{
	[SerializeField]
	private List<GUI_TextPanel> _textpanel = new List<GUI_TextPanel>();

	public void Init(List<Player> players, GameMode mode)
	{
		if (players.Count > 4)
		{
			Debug.LogWarning("More player then GUI_Textpanels.");
			return;
		}

		for(var i = 0; i < players.Count; i++)
		{
			_textpanel[i].gameObject.SetActive(true);
			_textpanel[i].Init(players[i], mode);
		}
	}
}
