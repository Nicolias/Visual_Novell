using UnityEngine;
using UnityEngine.UI;
using XNode;
using System;
using System.Collections.Generic;
using Zenject;
using TMPro;

[RequireComponent(typeof(Button))]
public class MessengerContact : MonoBehaviour
{
    [SerializeField] private Transform _chatsContainer;
    [SerializeField] private Chat _chatButtonTemplate;

    [SerializeField] private TMP_Text _contactNameText;

    [Inject] private DiContainer _di;
    [Inject] private Messenger _messenger;

    private Button _selfButton;

    private ContactElement _contactData;
    private List<Chat> _chatsList = new();

    private void Awake()
    {
        _selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(() => 
        _chatsContainer.gameObject.SetActive(!_chatsContainer.gameObject.activeInHierarchy));
    }

    public void Initialize(ContactElement contactElement)
    {
        _contactData = contactElement;
        _contactData.OnNewMessegeAdd += CreateNewChats;

        _chatsContainer.SetParent(transform.parent);

        _contactNameText.text = contactElement.Name;
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        if(_contactData != null)
            _contactData.OnNewMessegeAdd -= CreateNewChats;
    }

    private void CreateNewChats(NodeGraph newMessege, Action playActionAfterMessegeRed)
    {
        foreach (var chat in _chatsList)
        {
            if (chat.ChatData == newMessege)
                throw new InvalidOperationException("Chat already exist");
        }

        var newChat = _di.InstantiatePrefabForComponent<Chat>(_chatButtonTemplate, _chatsContainer);
        newChat.Initialize(newMessege, playActionAfterMessegeRed);
        _chatsList.Add(newChat);

        _messenger.AddUnreadChat(newChat);
    }
}
