using UnityEngine;
using UnityEngine.UI;
using XNode;
using System;
using System.Collections.Generic;
using Zenject;

[RequireComponent(typeof(Button))]
public class MessengerContact : MonoBehaviour
{
    [SerializeField] private Transform _chatsContainer;
    [SerializeField] private Chat _chatButtonTemplate;

    [Inject] private DiContainer _di;

    private Button _selfButton;

    private ContactElement _contactData;
    private List<Chat> _chatsList = new();

    private void Awake()
    {
        _selfButton = GetComponent<Button>();
    }

    public void Initialize(ContactElement contactElement)
    {
        _contactData = contactElement;
        _contactData.OnNewMessegeAdd += CreateNewChats;

        _chatsContainer.SetParent(transform.parent);

        foreach (var dialog in contactElement.Dialogs)
            CreateNewChats(dialog);
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(() => 
        _chatsContainer.gameObject.SetActive(!_chatsContainer.gameObject.activeInHierarchy));
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        _contactData.OnNewMessegeAdd -= CreateNewChats;
    }

    private void CreateNewChats(NodeGraph newMessege)
    {
        foreach (var chat in _chatsList)
        {
            if (chat.Data == newMessege)
                throw new InvalidOperationException("Chat already exist");
        }

        var newChat = _di.InstantiatePrefabForComponent<Chat>(_chatButtonTemplate, _chatsContainer);
        newChat.Initialize(newMessege); ;
        _chatsList.Add(newChat);
    }
}
