using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Messenger : MonoBehaviour
{
    public event Action OnNewMessegeRecived;
    public event Action OnAllMessegeRed;

    [SerializeField] private MessengerWindow _messengerWindow;
    [SerializeField] private Button _openMesengerButton;

    [SerializeField] private GameObject _unreadChatIndicator;

    [Inject] private ChatView _chatView;

    private List<ContactElement> _contacts = new();
    private List<Chat> _unreadChat = new();
    public IEnumerable<ContactElement> Contacts => _contacts;

    private void OnEnable()
    {
        _openMesengerButton.onClick.AddListener(() => _messengerWindow.Open());
    }

    private void OnDisable()
    {
        _openMesengerButton.onClick.RemoveAllListeners();
    }

    public void AddNewMessege(MessegeData newMessege, Action playActionAfterMessegeRed)
    {
        var existContact = _contacts.Find(x => x.Name == newMessege.ContactName);

        OnNewMessegeRecived?.Invoke();

        if (existContact != null)
        {
            existContact.AddMessege(newMessege, playActionAfterMessegeRed);
        }
        else
        {
            var newConatact = new ContactElement(newMessege.ContactName);
            _messengerWindow.CreateNewContactView(newConatact);

            newConatact.AddMessege(newMessege, playActionAfterMessegeRed);
            _contacts.Add(newConatact);
        }            
    }

    public void AddUnreadChat(Chat chat)
    {
        _unreadChat.Add(chat);
        _chatView.OnChatRed += OnChatRedCallBack;
        _unreadChatIndicator.SetActive(true);
    }

    private void OnChatRedCallBack(Chat chat)
    {
        _chatView.OnChatRed -= OnChatRedCallBack;
        _unreadChat.Remove(chat);

        if (_unreadChat.Count == 0)
        {
            OnAllMessegeRed?.Invoke();
            _unreadChatIndicator.SetActive(false);
        }
    }
}
