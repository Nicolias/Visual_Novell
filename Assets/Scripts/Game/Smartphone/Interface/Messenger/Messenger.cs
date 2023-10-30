using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using Zenject;

public class Messenger : MonoBehaviour, ISaveLoadObject
{
    [SerializeField] private Button _openMesengerButton;
    [SerializeField] private GameObject _unreadChatIndicator;

    private SaveLoadServise _saveLoadServise;
    private IMessengerWindow _messengerWindow;
    private IChatWindow _chatView;

    private List<Chat> _unreadChats = new();
    private List<MessegeData> _allMessages = new();

    private readonly string _saveKey = "MessengerSave";

    public IMessengerWindow MessengerWindow { get; private set; }
    public List<Chat> UnreadChats => new List<Chat>(_unreadChats);

    public event Action NewMessegeRecived;
    public event Action AllMessegeRead;
    public event Action<NodeGraph> ChatRead;

    [Inject]
    public void Construct(SaveLoadServise saveLoadServise, IChatWindow chatWindow, IMessengerWindow messengerWindow)
    {
        _saveLoadServise = saveLoadServise;
        _chatView = chatWindow;
        _messengerWindow = messengerWindow;
    }

    public void Construct(SaveLoadServise saveLoadServise, IChatWindow chatWindow, IMessengerWindow messengerWindow, GameObject unreadChatIndicator)
    {
        _saveLoadServise = saveLoadServise;
        _chatView = chatWindow;
        _messengerWindow = messengerWindow;
        _unreadChatIndicator = unreadChatIndicator;
    }

    private void OnEnable()
    {
        _chatView.ChatRead += OnChatRead;
        _openMesengerButton.onClick.AddListener(() => _messengerWindow.Show());

        if (_saveLoadServise.HasSave(_saveKey))
            Load();
    }

    private void OnDisable()
    {
        _chatView.ChatRead -= OnChatRead;
        _openMesengerButton.onClick.RemoveAllListeners();
        Save();
    }

    public void AddNewMessage(MessegeData newMessage)
    {
        if (_allMessages.Contains(newMessage))
            return;

        Chat chat = _messengerWindow.CreateChat(newMessage);
        _allMessages.Add(newMessage);

        AddToUnreadChat(chat);

        NewMessegeRecived?.Invoke();
    }

    private void AddToUnreadChat(Chat chat)
    {
        for (int i = 0; i < _unreadChats.Count; i++)
            if (_unreadChats[i].Data == chat.Data) 
                throw new InvalidOperationException("Чат уже существут");

        _unreadChats.Add(chat);
        _unreadChatIndicator.SetActive(true);
    }

    private void OnChatRead(Chat chat)
    {
        ChatRead?.Invoke(chat.Data);
        _unreadChats.Remove(chat);

        if (_unreadChats.Count == 0)
        {
            AllMessegeRead?.Invoke();
            _unreadChatIndicator.SetActive(false);
        }
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.IntData() { Int = _allMessages.Count });

        for (int i = 0; i < _allMessages.Count; i++)
        {
            _saveLoadServise.Save($"{_saveKey}/{i}", new SaveData.MessegeData()
            {
                Messege = _allMessages[i].Messege,
                ContactName = _allMessages[i].ContactName
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

            if (_allMessages.Exists(existMessage => existMessage.Messege == messege.Messege) == false)
            {
                _messengerWindow.CreateChat(messege);
                _allMessages.Add(messege);
            }
        }
    }
}
