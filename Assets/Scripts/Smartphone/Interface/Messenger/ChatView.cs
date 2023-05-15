using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using XNode;

public class ChatView : MonoBehaviour
{
    public event Action<List<ChatMessegeText>> OnChatViewClosed;

    private List<ChatMessegeText> _chatMesseges;

    [SerializeField] private TMP_Text _outputText;

    public void Show(List<ChatMessegeText> chatMesseges)
    {
        gameObject.SetActive(true);
        _chatMesseges = chatMesseges;
    }

    public void Close()
    {
        OnChatViewClosed?.Invoke(_chatMesseges);
        gameObject.SetActive(false);
    }
}

public class ChatMessegeText
{

}