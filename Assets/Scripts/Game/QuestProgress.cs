using System;
using System.Collections.Generic;
using Assets.Scripts.Game;
using Assets.Scripts.Interaction.Bhvrs;
using UnityEngine;
using UnityEngine.Events;

public class QuestProgress : MonoBehaviour, IQuestProgress, IGameEntities
{
    [SerializeField]
    private List<QuestEntityBhvr> _questEntityBhvrs = new List<QuestEntityBhvr>();
    private Quest _currentActiveQuest = default;
    private QuestBhvr _currentQuestBhvr = default;

    private List<UnityAction<QuestProgressArgs>> _questProgressEventListener = new List<UnityAction<QuestProgressArgs>>();

    public void AddQuestCompletedEventListener(UnityAction<QuestProgressArgs> args)
    {
        _questProgressEventListener.Add(args);
    }

    public void OnValidate()
    {
        _questEntityBhvrs.Clear();

        _questEntityBhvrs.AddRange(FindObjectsOfType<QuestEntityBhvr>());
    }

    private void Update()
    {
        if (_currentQuestBhvr != null)
        {
            _currentQuestBhvr.Update();
        }
    }

    public void SetNewQuest(Quest q)
    {
        if (q != null)
        {
            _currentActiveQuest = q;
            InitializeQuestBhvr(q);
            InitializeEventListener();
        }
    }

    private void InitializeEventListener()
    {
        if (_currentQuestBhvr != null)
        {
            foreach (UnityAction<QuestProgressArgs> action in _questProgressEventListener)
            {
                _currentQuestBhvr.AddQuestCompletedEventListener(action);
            }
        }
    }

    private void InitializeQuestBhvr(Quest q)
    {
        switch (q.GetQuestionMode())
        {
            case GameMode.Place:
                _currentQuestBhvr = new Quest_DragBhvr(q, this);
                break;
            case GameMode.Point:
                _currentQuestBhvr = new Quest_PointBhvr(q, this);
                break;
        }
    }

    public QuestEntityBhvr GetEntityBhvr(Entity entity)
    {
        var obj = _questEntityBhvrs.Find(q => q.Entity == entity);
        if (obj != null)
        {
            return obj;
        }
        else
        {
            return null;
        }
    }
}
