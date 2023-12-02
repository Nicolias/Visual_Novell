using UnityEngine;

namespace StateMachine
{
    public class PlayState : BaseState
    {
        private readonly CharactersLibrary _charactersLibrary;
        private readonly LocationsManager _locationsManager;
        private readonly AudioServise _audioServise;
        private readonly TimesOfDayServise _timesOfDayServise;

        private AudioClip _freePlaySound;

        public PlayState(AudioServise audioServise, AudioClip freePlaySound, TimesOfDayServise timesOfDayServise,
            CharactersLibrary charactersLibrary, LocationsManager locationsManager)
        {
            _charactersLibrary = charactersLibrary;
            _locationsManager = locationsManager;
            _audioServise = audioServise;
            _timesOfDayServise = timesOfDayServise;

            _freePlaySound = freePlaySound;
        }

        public override void Accept(IGameStateVisitor gameStateVisitor)
        {
            gameStateVisitor.Visit(this);
        }

        public override void Enter()
        {
            _audioServise.PlaySound(_freePlaySound);

            foreach (var character in _charactersLibrary.AllCharacters)
                if (character.ScriptableObject.CurrentLocation.TryGet(_timesOfDayServise.GetCurrentTimesOfDay(), out LocationSO locationSO))
                    if (_locationsManager.TryGet(locationSO, out ILocation location))
                        location.Set(character.ScriptableObject);
        }

        public override void Exit()
        {
            _audioServise.StopSound(_freePlaySound);
        }
    }
}
