using System;
using System.Collections.Generic;
using XNode;

namespace SaveData
{
    [Serializable]
    public class LocationData
    {
        public Node Quest;
        public List<ItemForCollection> Items;
    }
}