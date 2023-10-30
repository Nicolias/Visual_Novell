﻿using UnityEngine;
using XNode;

public class Messege
{
    public Sprite Avatar { get; private set; }
    public string Name { get; private set; }
    public string Text { get; private set; }
    public MessegeSenderType SenderType { get; private set; }

    public Node CurrentNode { get; private set; }

    public Messege(Sprite avatar, string name, string messegeText, MessegeSenderType senderType, Node currentNode)
    {
        Avatar = avatar;
        Name = name;
        Text = messegeText;
        SenderType = senderType;

        CurrentNode = currentNode;
    }
}