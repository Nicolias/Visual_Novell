using UnityEngine;

namespace StateMachine
{
    public class PlayState : BaseState
    {
        private AudioServise _audioServise;
        private AudioClip _freePlaySound;

        public PlayState(AudioServise audioServise, AudioClip freePlaySound)
        {
            _audioServise = audioServise;
            _freePlaySound = freePlaySound;
        }

        public override void Accept(IGameStateVisitor gameStateVisitor)
        {
            gameStateVisitor.Visit(this);
        }

        public override void Enter()
        {
            _audioServise.PlaySound(_freePlaySound);
        }

        public override void Exit()
        {
            _audioServise.StopSound(_freePlaySound);
        }
    }
}
