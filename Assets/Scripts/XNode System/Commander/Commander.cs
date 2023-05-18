using System;
using UnityEngine;
using XNode;
using Zenject;

public abstract class Commander : MonoBehaviour
{
    public event Action OnDialogEnd;

    [SerializeField] private NodeGraph _currentGraph;

    [Inject] protected StaticData StaticData;
    [Inject] protected CoroutineServise CoroutineServise;
    [Inject] protected AudioServise AudioServise;

    private (ICommand Command, Node Node) _curent;

    private void Start()
    {
        if (_currentGraph == null)
            return;

        PackAndExecuteCommand(_currentGraph.nodes[0]);
    }

    private void OnDisable()
    {
        if(_curent.Command != null)
            _curent.Command.OnComplete -= Next;
    }

    public void PackAndExecuteCommand(Node node)
    {
        if (node is ISpeechModel)
            (node as ISpeechModel).Initialize(StaticData);

        _curent = Packing(node);
        _curent.Command.OnComplete += Next;
        _curent.Command.Execute();
    }

    private void Next()
    {
        if (_curent.Command != null)
        {
            _curent.Command.OnComplete -= Next;
        }

        NodePort port = _curent.Node.GetPort("_outPut").Connection;

        if (port == null)
        {
            OnDialogEnd?.Invoke();
            return;
        }

        PackAndExecuteCommand(port.node);
    }

    protected abstract (ICommand, Node) Packing(Node node);
}
