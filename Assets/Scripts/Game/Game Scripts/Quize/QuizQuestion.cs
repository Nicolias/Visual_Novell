using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Create question for quiz")]
public class QuizQuestion : ScriptableObject
{
    [field: SerializeField] public CharacterType CharacterType { get; private set; }
    [field: SerializeField] public string Qustion { get; private set; }
    [field: SerializeField] public string CorrectAnswerText { get; private set; }
    [field: SerializeField] public List<string> UncorrectedAnswerText { get; private set; }
}