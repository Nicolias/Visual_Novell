using System;
using System.Collections.Generic;
using XNode;

public class ContactElement
{
    public event Action<NodeGraph> OnNewMessegeAdd;

    private List<NodeGraph> _dialogs;

    public string Name { get; private set; }
    public IEnumerable<NodeGraph> Dialogs => _dialogs;

    public ContactElement(string name, List<NodeGraph> dialogs)
    {
        Name = name;
        _dialogs = dialogs;
    }

    public void AddMessege(MessegeData newMessegeElement)
    {
        if (newMessegeElement.ContactName != Name)
            throw new InvalidOperationException("Сообщение не пренадлежит этому контакту");

        _dialogs.Add(newMessegeElement.Messege);

        OnNewMessegeAdd?.Invoke(newMessegeElement.Messege);
    }
}
