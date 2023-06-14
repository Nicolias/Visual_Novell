using System;

namespace SaveData
{
    [Serializable]
    public class BatterySaveData
    {
        public int ChargeLevel;

        public int LastOpenedYear;
        public int LastOpenedMonths;
        public int LastOpenedDay;
        public int LastOpenedHour;
        public int LastOpenedMinute;

    }
}