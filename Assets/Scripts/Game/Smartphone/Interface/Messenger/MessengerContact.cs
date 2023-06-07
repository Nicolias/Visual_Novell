using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

[RequireComponent(typeof(Button))]
public class MessengerContact : MonoBehaviour
{
    [SerializeField] private Transform _chatsContainer;
    [SerializeField] private TMP_Text _contactNameText;

    private Button _selfButton;

    private ContactElement _contactData;
    private List<Chat> _chatsList = new();

    public Transform ChatsContainer => _chatsContainer;

    private void Awake()
    {
        _selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        if (_selfButton != null & _chatsContainer != null)
            _selfButton.onClick.AddListener(Hide);            
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        if(_contactData != null)
            _contactData.OnNewChatAdded -= AddNewChatView;
    }

    public void Initialize(ContactElement contactElement)
    {
        _contactData = contactElement;

        _contactData.OnNewChatAdded += AddNewChatView;

        _chatsContainer.SetParent(transform.parent);

        _contactNameText.text = contactElement.Name;
    }

    public void Hide()
    {
        _chatsContainer.gameObject.SetActive(!_chatsContainer.gameObject.activeInHierarchy);
    }

    private void AddNewChatView(Chat newChat)
    {
        foreach (var chat in _chatsList)
        {
            if (chat.Data == newChat.Data)
                throw new InvalidOperationException("Chat already exist");
        }

        _chatsList.Add(newChat);
    }
}
