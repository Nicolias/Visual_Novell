using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using Zenject;

[RequireComponent(typeof(Button))]
public class Chat : MonoBehaviour
{
    [Inject] private ChatView _chatView;

    private Button _selfButton;

    private List<Messege> _messegesList = new();
    private Node _currentNode;

    private Action _playActionAfterMessegeRed;

    public NodeGraph ChatData { get; private set; }

    private void Awake()
    {
        _selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(OpenChat);
    }

    private void OnDisable()
    {
        _chatView.Close();
        _selfButton.onClick.RemoveAllListeners();
    }

    public void Initialize(NodeGraph chat, Action playActionAfterMessegeRed)
    {
        ChatData = chat;
        _currentNode = chat.nodes[0];
        _playActionAfterMessegeRed = playActionAfterMessegeRed;
    }

    private void OpenChat()
    {
        _chatView.ShowChat(_messegesList, _currentNode, this, _playActionAfterMessegeRed);
    }

    public void SaveChatData(List<Messege> chatMesseges, Node currentNode)
    {
        _messegesList = chatMesseges;

        NodePort port = currentNode.GetPort("_outPut").Connection;
        if (port == null)
        {
            _currentNode = null;
            return;
        }

        _currentNode = port.node;
    }
}
