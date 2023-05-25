using System;
using UnityEngine;
using XNode;
using Zenject;

public abstract class Commander : MonoBehaviour
{
    public event Action OnDialogEnd;

    [Inject] protected StaticData StaticData;
    [Inject] protected CoroutineServise CoroutineServise;
    [Inject] protected AudioServise AudioServise;    

    private (ICommand command, Node node) _curent;

    private void OnDisable()
    {
        if(_curent.command != null)
            _curent.command.OnComplete -= Next;
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
}
