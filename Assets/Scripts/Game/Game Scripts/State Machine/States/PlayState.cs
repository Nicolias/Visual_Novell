using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class PlayState : BaseState
    {
        private readonly CharactersLibrary _charactersLibrary;
        private readonly LocationsManager _locationsManager;
        private readonly AudioServise _audioServise;
        private readonly TimesOfDayServise _timesOfDayServise;

        private List<AudioClip> _freePlayMusicVariations;
        private AudioClip _currentMusic;

        public PlayState(AudioServise audioServise, List<AudioClip> freePlayMusicVariations, TimesOfDayServise timesOfDayServise,
            CharactersLibrary charactersLibrary, LocationsManager locationsManager)
        {
            _charactersLibrary = charactersLibrary;
            _locationsManager = locationsManager;
            _audioServise = audioServise;
            _timesOfDayServise = timesOfDayServise;

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

            foreach (var character in _charactersLibrary.AllCharacters)
                if (character.ScriptableObject.CurrentLocation.TryGet(_timesOfDayServise.GetCurrentTimesOfDay(), out LocationSO locationSO))
                    if (_locationsManager.TryGet(locationSO, out ILocation location))
                        location.Set(character.ScriptableObject);
        }

        public override void Exit()
        {
            _audioServise.StopSound(_currentMusic);
        }
    }
}
