using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using Zenject;

[RequireComponent(typeof(Button))]
public class Chat : MonoBehaviour
{
    [Inject] private IChatWindow _chatView;

    private Button _selfButton;

    private List<Messege> _messegasList = new();
    private Node _currentNode;

    public virtual NodeGraph Data { get; private set; }
    public IEnumerable<Messege> Messages => _messegasList;
    public Node CurrentNode => _currentNode;

    public event Action ChatRead;

    private void Awake()
    {
        _selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(OpenChat);
        _chatView.ChatRead += OnChatRead;
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
        _chatView.Close();
        _chatView.ChatRead -= OnChatRead;
    }

    public void Initialize(NodeGraph chat)
    {
        Data = chat;
        _currentNode = chat.nodes[0];
    }

    public void SaveChatData(Node currentNode)
    {
        NodePort port = currentNode.GetPort("_outPut").Connection;
        if (port == null)
        {
            _currentNode = null;
            return;
        }

        _currentNode = port.node;
    }

    public void Add(Messege newMessage)
    {
        if (newMessage != null)
            _messegasList.Add(newMessage);
    }

    private void OpenChat()
    {
        _chatView.Show(this);
    }

    private void OnChatRead(Chat chat)
    {
        if (chat == this)
            ChatRead?.Invoke();
    }
}
