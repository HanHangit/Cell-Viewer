using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Game
{
	public enum GameMode {  Place, Point}
	public List<Player> GetPlayerList() => _players;

	private GameMode _currentMod;
	private List<Player> _players;


	public Game()
	{
		_currentMod =  GameMode.Point;
		_players = new List<Player>();
	}


	public void AddPlayer()
	{
		_players.Add(new Player());
	}

	public void Start()
	{
		TextContainer textContainer = new TextContainer();
		var text= textContainer.LoadData(_currentMod);

		foreach (var player in _players)
		{
			player.StartGame(text);
		}
	}
}
