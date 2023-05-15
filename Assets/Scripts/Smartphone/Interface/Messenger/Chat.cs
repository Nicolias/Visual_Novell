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
    private List<ChatMessegeText> _chatMessegesData;

    public NodeGraph Data { get; private set; }

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
        Data = chat;
    }

    private void OpenChat()
    {
        _chatView.Close();
        _chatView.OnChatViewClosed += SaveChatData;
        _chatView.Show(_chatMessegesData);
    }

    private void SaveChatData(List<ChatMessegeText> chatMesseges)
    {
        _chatView.OnChatViewClosed -= SaveChatData;
    }
}
