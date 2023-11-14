using System.Collections.Generic;
using UnityEngine;

public class StaticData : MonoBehaviour
{
    [SerializeField] private List<QuizElement> _quizQuestion;

    private Dictionary<int, int> _needPointsToRichLevel = new()
    {
        { 2, 100 },
        { 3, 200 }
    };

    private int _currentAdsShowCount;
    private int _maxAdsShowCount = 20;

    private string _nickName = "Везунчик";

    [field: SerializeField] public string SpecWordForNickName { get; private set; }
    public string Nickname => _nickName;
    public int CurrentAdsShowCount => _currentAdsShowCount;
    public List<QuizElement> QuizQuestion => _quizQuestion;
    public bool ShowNextStory => _currentAdsShowCount >= _maxAdsShowCount;

    public int HowManyPointesNeedForReach(int level)
    {
        return _needPointsToRichLevel[level];
    }

    public void SetNickname(string nickname)
    {
        _nickName = nickname;
    }

    public void OnAdsShowed()
    {
        _currentAdsShowCount++;
    }

    public void Load(SaveData.BaseData data)
    {
        _nickName = data.String;
        _currentAdsShowCount = data.Int;
    }
}