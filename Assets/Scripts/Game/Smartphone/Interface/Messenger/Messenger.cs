using Factory.Messenger;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using Zenject;

public class Messenger : MonoBehaviour, ISaveLoadObject
{
    public event Action OnNewMessegeRecived;
    public event Action OnAllMessegeRed;
    public event Action<NodeGraph> OnChatRed;

    [Inject] private SaveLoadServise _saveLoadServise;

    [Inject] private ChatView _chatView;
    [Inject] private ChatFactory _chatFactory;

    [SerializeField] private MessengerWindow _messengerWindow;
    [SerializeField] private Button _openMesengerButton;

    [SerializeField] private GameObject _unreadChatIndicator;

    private List<ContactElement> _contacts = new();
    private List<Chat> _unreadChats = new();

    private List<MessegeData> _messegeDatas = new();
    private MessegeData _currentMessege;
    private const string _saveKey = "MessengerSave";

    private void OnEnable()
    {
        _openMesengerButton.onClick.AddListener(() => _messengerWindow.Show());

        if (_saveLoadServise.HasSave(_saveKey))
            Load();
    }

    private void OnDisable()
    {
        _openMesengerButton.onClick.RemoveAllListeners();
        Save();
    }

    public void AddNewMessege(MessegeData newMessege)
    {
        _currentMessege = newMessege;

        Chat chat = CreateChat(newMessege);

        AddUnreadChat(chat);

        OnNewMessegeRecived?.Invoke();
    }

    private Chat CreateChat(MessegeData newMessege)
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

        return chat;
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

        _messegeDatas.Add(_currentMessege);
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.IntData() { Int = _messegeDatas.Count });

        for (int i = 0; i < _messegeDatas.Count; i++)
        {
            _saveLoadServise.Save($"{_saveKey}/{i}", new SaveData.MessegeData()
            {
                Messege = _messegeDatas[i].Messege,
                ContactName = _messegeDatas[i].ContactName
            });
        }
    }

    public void Load()
    {
        var count = _saveLoadServise.Load<SaveData.IntData>(_saveKey).Int;

        for (int i = 0; i < count; i++)
        {
            var messegeData = _saveLoadServise.Load<SaveData.MessegeData>($"{_saveKey}/{i}");
            MessegeData messege = new(messegeData.ContactName, messegeData.Messege);

            CreateChat(messege);
            _messegeDatas.Add(messege);
        }
    }
}
