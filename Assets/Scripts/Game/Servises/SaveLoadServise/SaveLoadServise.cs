using System;
using UnityEngine;
using Unity.Services.CloudSave;
using System.Threading.Tasks;
using System.Collections.Generic;

public class SaveLoadServise : MonoBehaviour
{
    private Dictionary<string, Unity.Services.CloudSave.Models.Item> _results;

    private async void Awake()
    {
        _results = await CloudSaveService.Instance.Data.Player.LoadAllAsync();
    }

    public void Save<T>(string key, T saveData)
    {
        string jsonDataString = JsonUtility.ToJson(saveData, true);
        ForceSaveObjectData(key, jsonDataString);
    }

    public T Load<T>(string key) where T : new()
    {
        if (HasSave(key))
        {
            string loadedString = RetrieveSpecificData<string>(key);
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
        if (_results == null)
            return false;

        return _results.ContainsKey(key);
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

    private T RetrieveSpecificData<T>(string key)
    {
        try
        {
            if (_results.TryGetValue(key, out var item))
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
}
