using System;
using System.Collections.Generic;
using XNode;

public interface IChatWindow
{
    public event Action<Chat> ChatRead;

    public void Show(Chat currentChat);

    public void Close();

    public void Add(Messege newMessage);
}