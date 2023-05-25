using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using XNode;
using Zenject;

public class ChatView : MonoBehaviour
{
    public event Action<Chat> OnChatRed;

    [SerializeField] private Transform _messegeContainer;

    [Inject] private MessegeFactory _messegeFactory;
    [Inject] private MessengerCommander _messengerCommander;

    private Action _playActionAfterMessegeRed;

    private List<Messege> _chatMesseges;
    private Node _currentMessegeNode;
    private Chat _curruntChat;

    public void ShowChat(List<Messege> chatMesseges, Node currentNode, Chat currentChat, Action playActionAfterMessegeRed)
    {
        _messengerCommander.OnDialogEnd -= CallBack;
        _playActionAfterMessegeRed = playActionAfterMessegeRed;

        foreach (Transform messege in _messegeContainer)
            Destroy(messege.gameObject);

        _chatMesseges = chatMesseges;
        _curruntChat = currentChat;

        ShowOldMesseges();
        ContinueDialog(currentNode);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void CreateMessegeView(Messege messege)
    {
        _messegeFactory.CreateMessege(messege);
        _chatMesseges.Add(messege);

        _currentMessegeNode = messege.CurrentNode;
        _curruntChat.SaveChatData(_chatMesseges, _currentMessegeNode);
    }

    private void ShowOldMesseges()
    {
        gameObject.SetActive(true);

        foreach (Messege messege in _chatMesseges)
            _messegeFactory.CreateMessege(messege);
    }
    private void ContinueDialog(Node currentNode)
    {
        if (currentNode == null)
            return;

        _messengerCommander.OnDialogEnd += CallBack;
        _messengerCommander.PackAndExecuteCommand(currentNode);
    }

    private void CallBack()
    {
        _messengerCommander.OnDialogEnd -= CallBack;
        OnChatRed?.Invoke(_curruntChat);
        _playActionAfterMessegeRed.Invoke();
    }
}
