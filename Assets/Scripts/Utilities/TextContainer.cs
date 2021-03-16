using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;


[System.Serializable]
public class TextContainer
{
	private const string DRAG_MODE_PATH = "/Questions/DragQuestions.json";
	private const string POINT_MODE_PATH = "/Questions/PointQuestions.json";

	private void SaveData(List<string> data, string path)
	{
		string json = JsonConvert.SerializeObject(data, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
		File.WriteAllText(path, json);
	}

	private List<string> LoadData(string path)
	{
		return JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(path));
	}

	public List<string> LoadData(GameMode mode)
	{
		if (mode == GameMode.Point)
		{
			return LoadData(Application.dataPath + POINT_MODE_PATH);
		}
		else
		{
			return LoadData(Application.dataPath + DRAG_MODE_PATH);
		}
	}
}
