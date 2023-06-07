﻿public interface IItemCollector
{
    void Visit(BackpackItem backpackItem);
    void Visit(KeyItem keyItem);
    void Visit(StarItem starItem);
}