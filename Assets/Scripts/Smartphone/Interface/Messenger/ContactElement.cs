using System;
using System.Collections.Generic;
using XNode;

public class ContactElement
{
    public event Action<NodeGraph, Action> OnNewMessegeAdd;

    private List<NodeGraph> _dialogs = new();

    public string Name { get; private set; }
    public IEnumerable<NodeGraph> Dialogs => _dialogs;

    public ContactElement(string name)
    {
        Name = name;
    }

    public void AddMessege(MessegeData newMessegeElement, Action playActionAfterMessegeRed)
    {
        if (newMessegeElement.ContactName != Name)
            throw new InvalidOperationException("Сообщение не пренадлежит этому контакту");

        _dialogs.Add(newMessegeElement.Messege);

        OnNewMessegeAdd?.Invoke(newMessegeElement.Messege, playActionAfterMessegeRed);
    }
}
