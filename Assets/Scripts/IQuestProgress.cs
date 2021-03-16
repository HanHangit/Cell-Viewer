using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IQuestProgress
{
	void AddQuestCompletedEventListener(UnityAction<QuestProgressArgs> args);

	void SetNewQuest(Quest q);
}
