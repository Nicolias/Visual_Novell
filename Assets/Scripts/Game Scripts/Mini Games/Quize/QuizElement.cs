using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuizElement
{
    [field: SerializeField] public string Qustion { get; private set; }

    [field: SerializeField] public string CorrectAnswerText { get; private set; }
    [field: SerializeField] public List<string> UncorrectedAnswerText { get; private set; }
}