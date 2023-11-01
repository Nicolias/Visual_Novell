using System;
using UnityEngine;
using XNode;
using Zenject;

public class ChatWindow : MonoBehaviour, IChatWindow
{
    [SerializeField] private Transform _messegeContainer;

    private MessegeFactory _messageFactory;
    private MessengerCommander _messengerCommander;

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

        _messageFactory.HidePreviouslyCreatedMessages();
        _messageFactory.Show(chat.Messages);
        ContinueDialog(chat.CurrentNode);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Recieve(Messege newMessage, Action completeMessage)
    {
        _curruntChat.Add(newMessage);

        _curruntChat.SaveChatData(newMessage.CurrentNode);

        StartCoroutine(_messageFactory.Show(newMessage, completeMessage));
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
