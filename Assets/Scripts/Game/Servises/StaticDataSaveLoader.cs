using UnityEngine;
using Zenject;

public class StaticDataSaveLoader : MonoBehaviour, ISaveLoadObject
{
    private SaveLoadServise _saveLoadServise;
    private StaticData _staticData;

    private string _saveKey = "StaticDataSave";

    [Inject]
    public void Construct(SaveLoadServise saveLoadServise, StaticData staticData)
    {
        _saveLoadServise = saveLoadServise;
        _staticData = staticData;
        
        Add();
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

    public void Load()
    {
        var data = _saveLoadServise.Load<SaveData.BaseData>(_saveKey);
        _staticData.Load(data);
    }

    public void Add()
    {
        _saveLoadServise.Add(this);
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.BaseData() { String = _staticData.Nickname, Int = _staticData.CurrentAdsShowCount });
    }
}