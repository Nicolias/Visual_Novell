using System;
using UnityEngine;
using XNode;
using Zenject;

public abstract class Commander : MonoBehaviour, ISaveLoadObject
{
    public event Action OnDialogEnd;

    [Inject] protected readonly SaveLoadServise SaveLoadServise;
    [Inject] protected readonly StaticData StaticData;
    [Inject] protected readonly DiContainer DI;

    [Inject] protected readonly Battery Battery;
    [Inject] protected readonly Wallet Wallet;

    private (ICommand command, Node node) _curent;

    protected abstract string SaveKey { get; }

    private void OnDisable()
    {
        if (_curent.command != null)
            _curent.command.OnComplete -= Next;

        Save();
    }

    public void PackAndExecuteCommand(Node node)
    {
        _curent = Packing(node);
        _curent.command.OnComplete += Next;
        _curent.command.Execute();
    }

    private void Next()
    {
        if (_curent.command != null)
        {
            _curent.command.OnComplete -= Next;
        }

        NodePort port = _curent.node.GetPort("_outPut").Connection;

        if (port == null)
        {
            OnDialogEnd?.Invoke();
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

        PackAndExecuteCommand(_curent.node);
    }

    private SaveData.NodeData GetSaveSnapshot()
    {
        return new SaveData.NodeData()
        {
            Node = _curent.node,
        };
    }
}
