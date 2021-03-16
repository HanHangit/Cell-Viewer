using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUI_TextPanel : MonoBehaviour
{
	private Player _currentPlayer;

	[SerializeField]
	private TextMeshProUGUI _questionText = default;
	[SerializeField]
	private TextMeshProUGUI _gameModeText = default;
	[SerializeField]
	private TextMeshProUGUI _pointText = default;


	public void Init(Player player, Game.GameMode mode)
	{
		_currentPlayer = player;
		SetText(_pointText, 0);
		SetText(_gameModeText, mode.ToString());
		SetText(_questionText, "Will be initialize ... Please wait");

		player.PointChangedEvent.AddEventListener(PointChangeListener);
		player.TextChangedEvent.AddEventListener(TextChangeListener);
	}

	private void TextChangeListener(Player.TextArgs arg0)
	{
		SetText(_questionText, arg0.Newtext);
	}

	private void PointChangeListener(Player.PointArgs arg0)
	{
		SetText(_pointText, arg0.NewPoints);
	}

	private void SetText(TextMeshProUGUI textMesh, string txt)
	{
		textMesh.text = txt;
	}

	private void SetText(TextMeshProUGUI textMesh, int txt)
	{
		textMesh.text = txt.ToString();
	}
}
