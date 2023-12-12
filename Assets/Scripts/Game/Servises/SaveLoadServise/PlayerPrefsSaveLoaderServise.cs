using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPrefsSaveLoaderServise : SaveLoadServise
{
    public override event UnityAction Initialized;

    private void Awake()
    {
        Initialized?.Invoke();
    }

    public override void Save<T>(string key, T saveData)
    {
        string jsonDataString = JsonUtility.ToJson(saveData, true);
        PlayerPrefs.SetString(key, jsonDataString);
    }

    public override T Load<T>(string key)
    {
        if (HasSave(key))
        {
            string loadedString = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(loadedString);
        }
        else
        {
            throw new NotImplementedException($"Сохранения не существует {typeof(T)}");
        }
    }

    public override void ClearAllSave()
    {
        PlayerPrefs.DeleteAll();
    }

    public override bool HasSave(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
}