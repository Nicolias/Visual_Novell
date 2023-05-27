using System;
using UnityEngine;

[Serializable]
public class Character
{
    [SerializeField] private readonly CharacterSympathy _sympathy;

    [SerializeField] private CharacterType _characterType;

    public CharacterType CharacterType => _characterType;

    public Character(int sympathyPoints, int sympathyLevel, StaticData staticData, CharacterType characterType)
    {
        _sympathy = new(sympathyPoints, sympathyLevel, staticData);
        _characterType = characterType;
    }

    public void AccureSympathyPoints(int points)
    {
        if (points <= 0) throw new InvalidOperationException();

        _sympathy.AddPoints(points);
    }

    public void DecreesSympathyPoints(int points)
    {
        _sympathy.DecreesPoints(_sympathy.Points < points ? _sympathy.Points : points);
    }
}
