using UnityEngine;
using XNode;

public abstract class Commander : MonoBehaviour
{
    [SerializeField] private NodeGraph _currentGraph;

    private (ICommand Command, Node Node) _curent;

    private void Start()
    {
        _curent = Packing(_currentGraph.nodes[0]);
        _curent.Command.OnComplete += Next;
        _curent.Command.Execute();
    }

    private void OnDisable()
    {
        _curent.Command.OnComplete -= Next;
    }

    protected abstract (ICommand, Node) Packing(Node node);

    private void Next()
    {
        _curent.Command.OnComplete -= Next;
        NodePort port = _curent.Node.GetPort("_outPut").Connection;

        if (port == null) return;

        _curent = Packing(port.node);
        _curent.Command.OnComplete += Next;
        _curent.Command.Execute();
    }
}
