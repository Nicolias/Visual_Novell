using System;
using XNode;

namespace SaveData
{
    [Serializable]
    public class MessegeData
    {
        public string ContactName;
        public NodeGraph Messege;

        public bool IsUnread;
    }
}