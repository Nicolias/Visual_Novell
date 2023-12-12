using System;
using UnityEngine;
using Unity.Services.CloudSave;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.Events;

public class CloudSaveLoaderServise : SaveLoadServise
{
    private Dictionary<string, string> _localSaves = new Dictionary<string, string>();

    private Dictionary<string, string> _results = new Dictionary<string, string>();

    private bool _isCloudSaved = true;

    public override event UnityAction Initialized;

    public override async Task Initialize()
    {
        var dictionary = await CloudSaveService.Instance.Data.Player.LoadAllAsync();
        string jsonFile = RetrieveSpecificData<string>("CloudSave", dictionary);

        SerializableDictionary serializableDictionary = JsonUtility.FromJson<SerializableDictionary>(jsonFile);

        for (int i = 0; i < serializableDictionary.keys.Count; i++)
            _results.Add(serializableDictionary.keys[i], serializableDictionary.values[i]);

        Initialized?.Invoke();
    }


    public override void Save<T>(string key, T saveData)
    {
        string jsonDataString = JsonUtility.ToJson(saveData, true);

        if (_localSaves.ContainsKey(key))
            _localSaves[key] = jsonDataString;
        else
            _localSaves.Add(key, jsonDataString);

        if (_isCloudSaved)
            AsynkSave();
    }

    public override T Load<T>(string key)
    {
        if (HasSave(key))
        {
            string loadedString = _results[key];
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
        if (_results == null)
            return false;

        return _results.ContainsKey(key);
    }

    private async void AsynkSave()
    {
        _isCloudSaved = false;

        SerializableDictionary serializableDictionary = new SerializableDictionary();
        serializableDictionary.keys = new List<string>(_localSaves.Keys);
        serializableDictionary.values = new List<string>(_localSaves.Values);

        string data = JsonUtility.ToJson(serializableDictionary);
        await ForceSaveObjectData("CloudSave", data);

        _isCloudSaved = true;
    }

    private async Task<string> ForceSaveObjectData(string key, string value)
    {
        try
        {
            // Although we are only saving a single value here, you can save multiple keys
            // and values in a single batch.
            Dictionary<string, object> oneElement = new Dictionary<string, object>
                {
                    { key, value }
                };

            // Saving data without write lock validation by passing the data as an object instead of a SaveItem
            Dictionary<string, string> result = await CloudSaveService.Instance.Data.Player.SaveAsync(oneElement);
            string writeLock = result[key];

            Debug.Log($"Successfully saved {key}:{value} with updated write lock {writeLock}");

            return writeLock;
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }

        return null;
    }

    private T RetrieveSpecificData<T>(string key, Dictionary<string, Unity.Services.CloudSave.Models.Item> results)
    {
        try
        {
            if (results.TryGetValue(key, out var item))
            {
                return item.Value.GetAs<T>();
            }
            else
            {
                Debug.Log($"There is no such key as {key}!");
            }
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }

        return default;
    }

    [Serializable]
    public class SerializableDictionary
    {
        public List<string> keys;
        public List<string> values;
    }
}