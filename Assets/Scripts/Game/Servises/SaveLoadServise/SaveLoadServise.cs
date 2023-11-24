using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadServise : MonoBehaviour
{
    private List<ISaveLoadObject> _saveLoaderObjects = new List<ISaveLoadObject>();

    public void Add(ISaveLoadObject saveLoadObject)
    {
        _saveLoaderObjects.Add(saveLoadObject);
    }
    
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

    public void ClearAllSave()
    {
        PlayerPrefs.DeleteAll();
    }

    public bool HasSave(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public void SaveAll()
    {
        foreach (ISaveLoadObject saveLoadObject in _saveLoaderObjects)
            saveLoadObject.Save();
    }
}
