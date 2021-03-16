using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ASingleton<GameManager>
{
	private Game game = default;

	[SerializeField]
	private GUI_FieldController _fieldControllerPrefab = default;
	private GUI_FieldController _fieldControllerInstance = default;
	[SerializeField]
	private List<QuestProgress> _questProgress = default;

	protected override void SingletonAwake()
	{
		_fieldControllerInstance = Instantiate(_fieldControllerPrefab);
		game = new Game();

		for (int i = 0; i < 4; i++)
		{
			game.AddPlayer(_questProgress[i]);
		}

		_fieldControllerInstance.Init(game.GetPlayerList(), Game.GameMode.Point);
		game.Start();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			var player = game.GetPlayerList()[1];
			player.ChangeText();
		}
	}
}
