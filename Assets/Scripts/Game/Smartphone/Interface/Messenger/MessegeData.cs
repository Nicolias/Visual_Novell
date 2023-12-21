using System;
using UnityEngine;
using XNode;

[Serializable]
public class MessegeData
{
    [field : SerializeField] public string ContactName { get; private set; }
    [field : SerializeField] public string MessageName { get; private set; }
    [field: SerializeField] public NodeGraph Messege { get; private set; }
        
    public MessegeData(string contactName, string messageNmae, NodeGraph messege)
    {
        ContactName = contactName;
        MessageName = messageNmae;
        Messege = messege;
    }
}