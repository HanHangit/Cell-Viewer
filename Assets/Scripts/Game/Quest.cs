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
    private string _answer = default;
    public string GetAnswer() => _answer;
    [SerializeField]
    private Entity _targetEntity = default;
    [SerializeField]
    private GameMode _questionMode = default;

    public Entity GetTargetEntity() => _targetEntity;
    public GameMode GetQuestionMode() => _questionMode;
}
