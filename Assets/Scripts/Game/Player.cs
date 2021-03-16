using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	[SerializeField]
	private int _points = default;
	[SerializeField]
	private string _text = default;

	public GenericEvent<PointArgs> PointChangedEvent = new GenericEvent<PointArgs>();
	public GenericEvent<TextArgs> TextChangedEvent = new GenericEvent<TextArgs>();
	private GenerateRandomText _txtGenerator;

	public Player()
	{
		_points = 0;
		_text = "";
	}

	public void StartGame(List<string> text)
	{
		var list = new List<string>();
		foreach (var item in text)
		{
			list.Add(item);
		}
		_txtGenerator = new GenerateRandomText(list);
		ChangeText();
	}

	public void AddPoint()
	{
		var oldPoints = _points;
		_points++;
		PointChangedEvent.InvokeEvent(new PointArgs(oldPoints, _points));
	}

	public void ChangeText()
	{
		var oldText = _text;
		_text = _txtGenerator.GetRandomText();
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
