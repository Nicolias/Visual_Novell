using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class StaticData : MonoBehaviour, ISaveLoadObject
{
    [Inject] private SaveLoadServise _saveLoadServise;

    [SerializeField] private List<QuizElement> _quizQuestion;

    private Dictionary<int, int> _needPointsToRichLevel = new()
    {
        {2, 100},
        {3, 200 }
    };

    private const string _saveKey = "StaticDataSave";

    [field: SerializeField] public string SpecWordForNickName { get; private set; }
    public string Nickname { get; private set; }
    public List<QuizElement> QuizQuestion => _quizQuestion;

    private void Awake()
    {
        Nickname = "Везунчик";
    }

    private void OnEnable()
    {
        if (_saveLoadServise.HasSave(_saveKey))
            Load();
    }

    private void OnDisable()
    {
        Save();
    }

    public int HowManyPointesNeedForReach(int level)
    {
        return _needPointsToRichLevel[level];
    }

    public void SetNickname(string nickname)
    {
        Nickname = nickname;
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.StringData() { String = Nickname});
    }

    public void Load()
    {
        var data = _saveLoadServise.Load<SaveData.StringData>(_saveKey);
        Nickname = data.String;
    }
}
