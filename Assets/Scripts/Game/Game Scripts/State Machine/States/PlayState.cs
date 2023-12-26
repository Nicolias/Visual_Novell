using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class PlayState : BaseState
    {
        private readonly AudioServise _audioServise;

        private List<AudioClip> _freePlayMusicVariations;
        private AudioClip _currentMusic;

        public PlayState(AudioServise audioServise, List<AudioClip> freePlayMusicVariations)
        {
            _audioServise = audioServise;

            _freePlayMusicVariations = freePlayMusicVariations;
        }

        public override void Accept(IGameStateVisitor gameStateVisitor)
        {
            gameStateVisitor.Visit(this);
        }

        public override void Enter()
        {
            _currentMusic = _freePlayMusicVariations[Random.Range(0, _freePlayMusicVariations.Count)];

            _audioServise.PlaySound(_currentMusic);
        }

        public override void Exit()
        {
            _audioServise.StopSound(_currentMusic);
        }
    }
}
