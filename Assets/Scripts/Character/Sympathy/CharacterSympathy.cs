using System;


public class CharacterSympathy
{
    private readonly StaticData _staticData;

    private int _amountPoints, _level;

    public int SympathyLevel => _level;
    public int Points => _amountPoints;

    public CharacterSympathy(int currentPoint, int currentLevel, StaticData staticData)
    {
        _amountPoints = currentPoint;
        _level = currentLevel;

        _staticData = staticData;
    }

    public void AddPoints(int points)
    {
        if (points <= 0) throw new InvalidOperationException();

        _amountPoints += points;

        if (_amountPoints >= _staticData.HowManyPointesNeedForReach(_level + 1))
        {
            _amountPoints -= _staticData.HowManyPointesNeedForReach(_level);
            _level++;
        }
    }

    public void DecreesPoints(int points)
    {
        if(points < 0) throw new InvalidOperationException();
        if (_amountPoints - points < 0) throw new InvalidOperationException("Симпатия ушла в отрицательное значение");

        _amountPoints -= points;
    }
}
