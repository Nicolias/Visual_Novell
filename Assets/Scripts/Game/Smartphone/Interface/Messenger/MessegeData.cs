using System;
using UnityEngine;
using XNode;

[Serializable]
public class MessegeData
{
    [field : SerializeField] public string ContactName { get; private set; }
    [field: SerializeField] public NodeGraph Messege { get; private set; }
        
    public MessegeData(string contactName, NodeGraph messege)
    {
        ContactName = contactName;
        Messege = messege;
    }
}