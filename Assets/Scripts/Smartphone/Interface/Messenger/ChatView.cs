using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using XNode;
using Zenject;

public class ChatView : MonoBehaviour
{
    public event Action<List<Messege>, Node> OnChatViewClosed;

    [SerializeField] private Transform _messegeContainer;

    [Inject] private MessegeFactory _messegeFactory;
    [Inject] private MessengerCommander _messengerCommander;

    private List<Messege> _chatMesseges;
    private Node _currentMessegeNode;

    public void ShowChat(List<Messege> chatMesseges, Node currentNode)
    {
        foreach (Transform messege in _messegeContainer)
            Destroy(messege.gameObject);

        _chatMesseges = chatMesseges;

        ShowOldMesseges();
        ContinueDialog(currentNode);
    }
    public void Close()
    {
        OnChatViewClosed?.Invoke(_chatMesseges, _currentMessegeNode);
        gameObject.SetActive(false);
    }

    public void CreateMessegeView(Messege messege)
    {
        _messegeFactory.CreateMessege(messege);
        _chatMesseges.Add(messege);

        _currentMessegeNode = messege.CurrentNode;
    }

    private void ShowOldMesseges()
    {
        gameObject.SetActive(true);

        foreach (Messege messege in _chatMesseges)
            _messegeFactory.CreateMessege(messege);
    }
    private void ContinueDialog(Node currentNode)
    {
        if (currentNode == null)
            return;

        _messengerCommander.PackAndExecuteCommand(currentNode);
    }
}
