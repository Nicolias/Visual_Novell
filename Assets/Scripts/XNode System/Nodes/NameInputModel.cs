﻿using TMPro;
using UnityEngine;
using XNode;

public class NameInputModel : XnodeModel, INicknameInputModel
{
    [SerializeField] private TMP_InputField _nicknameInput;

    [SerializeField]
    [Output(dynamicPortList = true)]
    [TextArea(1, 2)]
    private string[] _textChoices;

    public string[] Choices => _textChoices;
    public Node[] Nodes => GetNodes();
    public TMP_InputField InputNickname => _nicknameInput;

    public void SetEndPort(Node node)
    {
        NodePort node1 = GetOutputPort("_outPut");
        NodePort node2 = node.GetInputPort("_inPut");
        node1.ClearConnections();
        node1.Connect(node2);
    }

    private Node[] GetNodes()
    {
        Node[] result = new Node[_textChoices.Length];

        for (int i = 0; i < _textChoices.Length; i++)
            result[i] = GetPort($"{nameof(_textChoices)} {i}").Connection.node;

        return result;
    }

}