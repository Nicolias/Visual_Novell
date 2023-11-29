using System;
using UnityEngine.Events;

namespace Characters
{
    public interface ICharacter : IDataForCell, IDisposable
    {
        public CharacterType Type { get; }
        public int SympathyPoints { get; }
        public int SympathyLevel { get; }

        public bool IsMeetingWithPlayer { get; }
        public CharacterSO ScriptableObject { get; }

        public event UnityAction<int> SympathyPointsChanged;

        public void AccureSympathyPoints(int points);

        public void DecreesSympathyPoints(int points);

        public void Feed(EatingProduct product);
        public void Invite(Location location, int meetingSympathyBonus);

        public void MeetWithPlayer();
    }
}