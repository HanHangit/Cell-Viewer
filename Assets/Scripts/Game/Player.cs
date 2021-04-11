using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player
{
	private int _points = default;
    private string _text = default;

    private IQuestProgress _questProgress;
    private GenerateRandomQuest _questGenerator;
    private Quest _currentQuest = default;

    public GenericEvent<PointArgs> PointChangedEvent = new GenericEvent<PointArgs>();
    public GenericEvent<TextArgs> ShowSolutionEvent = new GenericEvent<TextArgs>();
    public GenericEvent<TextArgs> TextChangedEvent = new GenericEvent<TextArgs>();


    public Player(IQuestProgress progress)
    {
        _questProgress = progress;
        _questProgress.AddQuestCompletedEventListener(QuestCompletedListener);

        _points = 0;
        _text = "";
    }

    public void SetNewQuest()
    {
	    _currentQuest = _questGenerator.GetRandomQuest();
	    SetNewQuest(_currentQuest);
    }

    private void QuestCompletedListener(QuestProgressArgs arg0)
    {
        AddPoint();
        ShowConclusion();
    }

    private void ShowConclusion()
    {
	    ShowSolutionEvent.InvokeEvent(new TextArgs("",_currentQuest.GetAnswer()));
    }

    public void StartGame()
    {
        _questGenerator = new GenerateRandomQuest();
        SetNewQuest();
    }

    private void SetNewQuest(Quest quest)
    {
        if (quest != null)
        {
            _questProgress.SetNewQuest(quest);
            ChangeText(quest.GetDescription());
        }
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
