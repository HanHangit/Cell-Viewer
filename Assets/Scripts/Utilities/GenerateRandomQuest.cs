using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateRandomQuest
{
	private List<Quest> _quests = new List<Quest>();


	public GenerateRandomQuest()
	{
		string path = "Quests";
		_quests = Resources.LoadAll<Quest>(path).ToList();
	}

	public Quest GetRandomQuest()
	{
		if (_quests.Count == 0)
		{
			return null;
		}
		else
		{
			var index = UnityEngine.Random.Range(0, _quests.Count - 1);
			var element = _quests[index];
			_quests.Remove(element);
			return element;
		}
	}
}

