using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
	[SerializeField]
	private string _description = default;
	[SerializeField]
	private Entity _targetEntity = default;

	public Entity GetTargetEntity() => _targetEntity;

	public bool Validate(Entity entity)
	{
		return true;
	}
}
