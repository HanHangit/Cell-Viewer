using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUI_TextPanel : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _questionText = default;
	[SerializeField]
	private TextMeshProUGUI _gameModeText = default;
	[SerializeField]
	private TextMeshProUGUI _pointText = default;
	[SerializeField]
	private Button _nextQuestButton = default;
	private Player _currentPlayer = default;

	public GenericEvent<NextButtonEventArgs> NextButtonPressedEvent = new GenericEvent<NextButtonEventArgs>();
	public void Init(Player player, GameMode mode)
	{
		_currentPlayer = player;
		_nextQuestButton.gameObject.SetActive(false);
		_nextQuestButton.onClick.AddListener(ButtonClickedListener);
		SetText(_pointText, 0);
		SetText(_gameModeText, mode.ToString());
		SetText(_questionText, "Will be initialize ... Please wait");

		player.PointChangedEvent.AddEventListener(PointChangeListener);
		player.TextChangedEvent.AddEventListener(TextChangeListener);
		player.ShowSolutionEvent.AddEventListener(ShowSolutionListener);
	}

	private void ButtonClickedListener()
	{
		NextButtonPressedEvent.InvokeEvent(new NextButtonEventArgs(_currentPlayer), this);
		_nextQuestButton.gameObject.SetActive(false);
	}

	private void ShowSolutionListener(Player.TextArgs arg0)
	{
		_nextQuestButton.gameObject.SetActive(true);
		SetText(_questionText, arg0.Newtext);
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

	public struct NextButtonEventArgs
	{
		public Player Player;

		public NextButtonEventArgs(Player player)
		{
			Player = player;
		}
	}

}
