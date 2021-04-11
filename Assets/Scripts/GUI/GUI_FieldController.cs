using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_FieldController : MonoBehaviour
{
	[SerializeField]
	private List<GUI_TextPanel> _textpanel = new List<GUI_TextPanel>();
	private Player _currPlayer = default;

	public void Init(List<Player> players, GameMode mode)
	{
		if (players.Count > 4)
		{
			Debug.LogWarning("More player then GUI_Textpanels.");
			return;
		}

		_currPlayer = players[0];
		for (var i = 0; i < players.Count; i++)
		{
			_textpanel[i].gameObject.SetActive(true);
			_textpanel[i].Init(_currPlayer, mode);
		}
		_textpanel[0].NextButtonPressedEvent.AddEventListener(NextQuestPressedListener);
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.B))
		{
			NextQuestPressedListener(new GUI_TextPanel.NextButtonEventArgs());
		}
	}

	private void NextQuestPressedListener(GUI_TextPanel.NextButtonEventArgs arg0)
	{
		if (arg0.Player != null)
		{
			arg0.Player.SetNewQuest();
		}
		else
		{
			_currPlayer.SetNewQuest();
		}
	}
}
