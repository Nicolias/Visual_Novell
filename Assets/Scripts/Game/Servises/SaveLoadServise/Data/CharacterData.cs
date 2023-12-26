using System;

namespace SaveData
{
    [Serializable]
    public class CharacterData
    {
        public int SympathyPoints;
        public int SympathyLevel;
        public bool IsMeeting;

        public int LastEatingTimeOfDay;
        public int LastEatingTimeYear;
        public int LastEatingTimeMonth;
        public int LastEatingTimeDay;
    }
}