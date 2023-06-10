using System;
using UnityEngine;

public class SaveLoadServise : MonoBehaviour
{
    public void Save<T>(string key, T saveData)
    {
        string jsonDataString = JsonUtility.ToJson(saveData, true);
        PlayerPrefs.SetString(key, jsonDataString);
    }

    public T Load<T>(string key) where T : new()
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

    public bool HasSave(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
}
