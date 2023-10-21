using System;

public abstract class AbstractMiniGame
{
    public abstract event Action Drawn;
    public abstract event Action CharacterWon;
    public abstract event Action CharacterLost;
}
