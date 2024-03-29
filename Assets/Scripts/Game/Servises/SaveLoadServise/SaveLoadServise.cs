﻿using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public abstract class SaveLoadServise : MonoBehaviour
{
    public abstract int SaveLoadCount { get; }

    public abstract event UnityAction Initialized;

    public virtual async Task Initialize() { }

    public abstract void Save<T>(string key, T saveData);

    public abstract T Load<T>(string key) where T : new();

    public abstract void ClearAllSave();

    public abstract bool HasSave(string key);

    public abstract void SaveAll();

    public abstract void Add(ISaveLoadObject audioServiseSaveLoader);
}
