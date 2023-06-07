using UnityEngine;
using System.Collections.Generic;

public class StaticData : MonoBehaviour
{
    [SerializeField] private List<QuizElement> _quizQuestion;

    private Dictionary<int, int> _needPointsToRichLevel = new()
    {
        { 2, 100},
        {3, 200 }
    }; 

    [field: SerializeField] public string SpecWordForNickName { get; private set; }
    public string Nickname { get; private set; }
    public List<QuizElement> QuizQuestion => _quizQuestion;

    private void Awake()
    {
        Nickname = "Везунчик";
    }

    public int HowManyPointesNeedForReach(int level)
    {
        return _needPointsToRichLevel[level];
    }

    public void SetNickname(string nickname)
    {
        Nickname = nickname;
    }
}
