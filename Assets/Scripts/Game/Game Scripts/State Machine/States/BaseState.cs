﻿namespace StateMachine
{
    public abstract class BaseState
    {
        public abstract void Enter();
        public abstract void Exit();

        public abstract void Accept(IGameStateVisitor gameStateVisitor);
    }
}