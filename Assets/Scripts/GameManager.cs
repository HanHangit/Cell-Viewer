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

	protected override void SingletonAwake()
	{
		_fieldControllerInstance = Instantiate(_fieldControllerPrefab);
		game = new Game();
		game.AddPlayer();
		game.AddPlayer();
		game.AddPlayer();
		game.AddPlayer();
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
