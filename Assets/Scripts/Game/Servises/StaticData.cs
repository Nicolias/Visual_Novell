using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class StaticData : MonoBehaviour, ISaveLoadObject
{
    private const string _saveKey = "StaticDataSave";

    [SerializeField] private List<QuizElement> _quizQuestion;
    [SerializeField] private string _nickname = "��������";
    [Inject] private SaveLoadServise _saveLoadServise;

    private Dictionary<int, int> _needPointsToRichLevel = new()
    {
        {2, 100},
        {3, 200 }
    };

    [field: SerializeField] public bool IsSkipDialog { get; private set; }
    [field: SerializeField] public string SpecWordForNickName { get; private set; }

    public string Nickname => _nickname;
    public List<QuizElement> QuizQuestion => _quizQuestion;

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
        _nickname = nickname;
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.StringData() { String = Nickname});
    }

    public void Load()
    {
        var data = _saveLoadServise.Load<SaveData.StringData>(_saveKey);
        _nickname = data.String;
    }
}