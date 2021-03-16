using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    [SerializeField]
    private string _description = default;
    public string GetDescription() => _description;
    [SerializeField]
    private Entity _targetEntity = default;
    [SerializeField]
    private Entity _targetDragEntity = default;
    [SerializeField]
    private GameMode _questionMode = default;

    public Entity TargetDragEntity => _targetDragEntity;
    public Entity GetTargetEntity() => _targetEntity;
    public GameMode GetQuestionMode() => _questionMode;
}
