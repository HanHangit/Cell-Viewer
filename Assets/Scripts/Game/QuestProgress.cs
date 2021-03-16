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

    public void AddQuestCompletedEventListener(UnityAction<QuestProgressArgs> args)
    {

    }

    public void SetNewQuest(Quest q)
    {
        _currentActiveQuest = q;
        InitializeQuestBhvr(q);
    }

    private void InitializeQuestBhvr(Quest q)
    {
        switch (q.GetQuestionMode())
        {
            case GameMode.Place:
                _currentQuestBhvr = new Quest_DragBhvr(q, this);
                break;
            case GameMode.Point:
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

