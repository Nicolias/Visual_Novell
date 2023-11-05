using System;

public interface ICloseable
{
    public event Action Closed;
}
