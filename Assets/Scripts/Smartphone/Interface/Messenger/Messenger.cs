using Factory.Messenger;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using Zenject;

public class Messenger : MonoBehaviour
{
    public event Action OnNewMessegeRecived;
    public event Action OnAllMessegeRed;
    public event Action<NodeGraph> OnChatRed;

    [SerializeField] private MessengerWindow _messengerWindow;
    [SerializeField] private Button _openMesengerButton;

    [SerializeField] private GameObject _unreadChatIndicator;

    [Inject] private ChatView _chatView;
    [Inject] private ChatFactory _chatFactory;


    private List<ContactElement> _contacts = new();
    private List<Chat> _unreadChats = new();

    private void OnEnable()
    {
        _openMesengerButton.onClick.AddListener(() => _messengerWindow.Show());
    }

    private void OnDisable()
    {
        _openMesengerButton.onClick.RemoveAllListeners();
    }

    public void AddNewMessege(MessegeData newMessege)
    {
        ContactElement existContact = _contacts.Find(x => x.Name == newMessege.ContactName);
        MessengerContact currentContactView = null;

        if (existContact == null)
        {
            ContactElement newConatactData = new(newMessege.ContactName);
            _contacts.Add(newConatactData);

            currentContactView = _messengerWindow.CreateContactView(newConatactData);
        }
        else
        {
            currentContactView = _messengerWindow.GetExistContactView(existContact);
        }

        var chat = _chatFactory.Create(newMessege.Messege, currentContactView.ChatsContainer);
        AddUnreadChat(chat);

        OnNewMessegeRecived?.Invoke();
    }

    private void AddUnreadChat(Chat chat)
    {
        for (int i = 0; i < _unreadChats.Count; i++)
            if (_unreadChats[i].Data == chat.Data) throw new InvalidOperationException("Чат уже существут");

        _unreadChats.Add(chat);
        _chatView.OnChatRed += OnChatRedCallBack;
        _unreadChatIndicator.SetActive(true);
    }

    private void OnChatRedCallBack(Chat chat)
    {
        OnChatRed?.Invoke(chat.Data);
        _chatView.OnChatRed -= OnChatRedCallBack;
        _unreadChats.Remove(chat);

        if (_unreadChats.Count == 0)
        {
            OnAllMessegeRed?.Invoke();
            _unreadChatIndicator.SetActive(false);
        }
    }

}
