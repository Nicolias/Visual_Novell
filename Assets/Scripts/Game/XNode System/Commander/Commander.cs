using System;
using UnityEngine;
using XNode;
using Zenject;

public abstract class Commander : MonoBehaviour, ISaveLoadObject
{
    public event Action DialogEnded;

    [Inject] protected readonly SaveLoadServise SaveLoadServise;
    [Inject] protected readonly StaticData StaticData;
    [Inject] protected readonly DiContainer DI;

    [Inject] protected readonly Battery Battery;
    [Inject] protected readonly Wallet Wallet;

    private (ICommand command, Node node) _curent;

    protected abstract string SaveKey { get; }

    private void Awake()
    {
        Add();
    }

    private void OnDisable()
    {
        if (_curent.command != null)
            _curent.command.Completed -= Next;
    }

    public void PackAndExecuteCommand(Node node)
    {
        _curent = Packing(node);
        _curent.command.Completed += Next;
        _curent.command.Execute();
    }

    private void Next()
    {
        SaveLoadServise.SaveAll();

        if (_curent.command != null)
        {
            _curent.command.Completed -= Next;
        }

        NodePort port = _curent.node.GetPort("_outPut").Connection;

        if (port == null)
        {
            DialogEnded?.Invoke();
            _curent.node = null;
            return;
        }

        PackAndExecuteCommand(port.node);
    }

    protected abstract (ICommand, Node) Packing(Node node);

    public void Save()
    {
        SaveLoadServise.Save(SaveKey, GetSaveSnapshot());
    }

    public void Load()
    {
        var data = SaveLoadServise.Load<SaveData.NodeData>(SaveKey);
        _curent.node = data.Node;

        if (data.Node != null)
            PackAndExecuteCommand(_curent.node);
    }

    public void Add()
    {
        SaveLoadServise.Add(this);
    }

    private SaveData.NodeData GetSaveSnapshot()
    {
        return new SaveData.NodeData()
        {
            Node = _curent.node,
        };
    }
}
