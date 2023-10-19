using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Zenject;

public class ChatWindow : MonoBehaviour, IChatWindow
{
    [SerializeField] private Transform _messegeContainer;

    private MessegeFactory _messageFactory;
    private MessengerCommander _messengerCommander;

    private Node _currentMessegeNode;
    private Chat _curruntChat;

    public event Action<Chat> ChatRead;

    [Inject]
    public void Construct(MessegeFactory messageFactory, MessengerCommander messengerCommander)
    {
        _messageFactory = messageFactory;
        _messengerCommander = messengerCommander;
    }

    public void Show(Chat chat)
    {
        gameObject.SetActive(true);

        _messengerCommander.DialogEnded -= OnDialogFinished;

        _curruntChat = chat;

        foreach (Transform messege in _messegeContainer)
            Destroy(messege.gameObject);

        ShowInChat(_curruntChat.Messages);
        ContinueDialog(chat.CurrentNode);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Add(Messege newMessege)
    {
        _curruntChat.Add(newMessege);

        _currentMessegeNode = newMessege.CurrentNode;
        _curruntChat.SaveChatData(_currentMessegeNode);

        _messageFactory.CreateMessegeView(newMessege);
    }

    private void ShowInChat(IEnumerable<Messege> messages)
    {
        gameObject.SetActive(true);

        foreach (Messege messege in messages)
            _messageFactory.CreateMessegeView(messege);
    }

    private void ContinueDialog(Node currentNodeInDialog)
    {
        if (currentNodeInDialog == null)
            return;

        _messengerCommander.DialogEnded += OnDialogFinished;
        _messengerCommander.PackAndExecuteCommand(currentNodeInDialog);
    }

    private void OnDialogFinished()
    {
        _messengerCommander.DialogEnded -= OnDialogFinished;
        ChatRead?.Invoke(_curruntChat);
    }
}
