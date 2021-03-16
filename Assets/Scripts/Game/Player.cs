using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;

public class Player
{
	[SerializeField]
	private int _points = default;
	[SerializeField]
	private string _text = default;

	private IQuestProgress _questProgress;
	public GenericEvent<PointArgs> PointChangedEvent = new GenericEvent<PointArgs>();
	public GenericEvent<TextArgs> TextChangedEvent = new GenericEvent<TextArgs>();
	private GenerateRandomQuest _questGenerator;

	public Player(IQuestProgress progress)
	{
		_questProgress = progress;
		_questProgress.AddQuestCompletedEventListener(QuestCompletedListener);

		_points = 0;
		_text = "";
	}

	private void QuestCompletedListener(QuestProgressArgs arg0)
	{
		AddPoint();
		var quest = _questGenerator.GetRandomQuest();
		SetNewQuest(quest);
	}

	public void StartGame()
	{
		_questGenerator = new GenerateRandomQuest();
		var quest = _questGenerator.GetRandomQuest();
		SetNewQuest(quest);
	}

	private void SetNewQuest(Quest quest)
	{
		_questProgress.SetNewQuest(quest);
		ChangeText(quest.GetDescription());
	}

	public void AddPoint()
	{
		var oldPoints = _points;
		_points++;
		PointChangedEvent.InvokeEvent(new PointArgs(oldPoints, _points));
	}

	public void ChangeText(string txt)
	{
		var oldText = _text;
		_text = txt;
		TextChangedEvent.InvokeEvent(new TextArgs(oldText, _text));
	}

	public struct PointArgs
	{
		public int OldPoints;
		public int NewPoints;

		public PointArgs(int oldPoints, int newPoints)
		{
			this.OldPoints = oldPoints;
			this.NewPoints = newPoints;
		}
	}

	public struct TextArgs
	{
		public string OldText;
		public string Newtext;

		public TextArgs(string oldText, string newtext)
		{
			OldText = oldText;
			Newtext = newtext;
		}
	}


}
