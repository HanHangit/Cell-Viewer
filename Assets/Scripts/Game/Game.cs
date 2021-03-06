﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Game
{

	public List<Player> GetPlayerList() => _players;

	private GameMode _currentMod;
	private List<Player> _players;


	public Game()
	{
		_currentMod =  GameMode.Point;
		_players = new List<Player>();
	}


	public void AddPlayer(IQuestProgress progress)
	{
		_players.Add(new Player(progress));
	}

	public void Start()
	{

		foreach (var player in _players)
		{
			player.StartGame();
		}
	}
}
