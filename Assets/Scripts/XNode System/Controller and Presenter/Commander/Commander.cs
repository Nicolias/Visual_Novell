using UnityEngine;
using XNode;

public abstract class Commander : MonoBehaviour
{
    [SerializeField] private NodeGraph _currentGraph;

    private (ICommand Command, Node Node) _curent;

    private void Start()
    {
        if (_currentGraph == null)
            return;

        PackAndExecuteCommand(_currentGraph.nodes[0]);
    }

    private void OnDisable()
    {
        _curent.Command.OnComplete -= Next;
    }

    private void Next()
    {
        if (_curent.Command != null)
        {
            _curent.Command.OnComplete -= Next;
        }

        NodePort port = _curent.Node.GetPort("_outPut").Connection;

        if (port == null) return;

        PackAndExecuteCommand(port.node);
    }

    protected void PackAndExecuteCommand(Node node)
    {
        _curent = Packing(node);
        _curent.Command.OnComplete += Next;
        _curent.Command.Execute();
    }

    protected abstract (ICommand, Node) Packing(Node node);
}
