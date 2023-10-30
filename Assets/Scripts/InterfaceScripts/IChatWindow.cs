using System;

public interface IChatWindow
{
    public event Action<Chat> ChatRead;

    public void Show(Chat currentChat);

    public void Close();

    public void Recieve(Messege newMessage, Action completeMessage);
}