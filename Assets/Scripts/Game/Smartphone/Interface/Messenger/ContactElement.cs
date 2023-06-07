using System;
using System.Collections.Generic;
using XNode;

public class ContactElement
{
    public event Action<Chat> OnNewChatAdded;

    private List<NodeGraph> _dialogs = new();

    public string Name { get; private set; }
    public IEnumerable<NodeGraph> Dialogs => _dialogs;

    public ContactElement(string name)
    {
        Name = name;
    }

    public void AddMessege(Chat chat)
    {
        _dialogs.Add(chat.Data);

        OnNewChatAdded?.Invoke(chat);
    }
}
