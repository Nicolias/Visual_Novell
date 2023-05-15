using System.Collections.Generic;
using TMPro;
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
        _chatView.OnChatViewClosed -= SaveChatData;
    }

    public void Initialize(NodeGraph chat)
    {
        ChatData = chat;
        _currentNode = chat.nodes[0];
    }

    private void OpenChat()
    {
        _chatView.Close();
        _chatView.OnChatViewClosed += SaveChatData;

        _chatView.ShowChat(_messegesList, _currentNode);
    }

    private void SaveChatData(List<Messege> chatMesseges, Node currentNode)
    {
        _chatView.OnChatViewClosed -= SaveChatData;

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
