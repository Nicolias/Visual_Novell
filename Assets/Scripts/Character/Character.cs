using System;
using UnityEngine;

namespace Characters
{
    [Serializable]
    public class Character
    {
        public event Action<int> OnSympathyPointsChanged;

        [SerializeField] private readonly CharacterSympathy _sympathy;

        [SerializeField] private CharacterType _characterType;

        public CharacterType CharacterType => _characterType;
        public int SympathyPoints => _sympathy.Points;

        public Character(int sympathyPoints, int sympathyLevel, StaticData staticData, CharacterType characterType)
        {
            _sympathy = new(sympathyPoints, sympathyLevel, staticData);
            _characterType = characterType;
        }

        public void AccureSympathyPoints(int points)
        {
            if (points <= 0) throw new InvalidOperationException();
            _sympathy.AddPoints(points);

            OnSympathyPointsChanged?.Invoke(_sympathy.Points);
        }

        public void DecreesSympathyPoints(int points)
        {
            _sympathy.DecreesPoints(_sympathy.Points < points ? _sympathy.Points : points);

            OnSympathyPointsChanged?.Invoke(_sympathy.Points);
        }
    }
}