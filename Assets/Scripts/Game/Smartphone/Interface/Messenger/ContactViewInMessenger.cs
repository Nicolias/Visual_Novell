using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

[RequireComponent(typeof(Button))]
public class ContactViewInMessenger : MonoBehaviour
{
    [SerializeField] private Transform _chatsContainer;
    [SerializeField] private TMP_Text _contactNameText;

    private Button _selfButton;

    private List<Chat> _chatsList = new();

    public Transform ChatsContainer => _chatsContainer;
    public int ChatsCount => _chatsList.Count;

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

    public void Initialize(ContactData contact)
    {
        _chatsContainer.SetParent(transform.parent);

        _contactNameText.text = contact.Name;
    }

    public void Hide()
    {
        _chatsContainer.gameObject.SetActive(!_chatsContainer.gameObject.activeInHierarchy);
    }

    public void Add(Chat newChat)
    {
        foreach (var chat in _chatsList)
        {
            if (chat.Data == newChat.Data)
                throw new InvalidOperationException("Chat already exist");
        }

        _chatsList.Add(newChat);
    }
}
