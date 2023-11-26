using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace StateMachine
{
    public class GameStateMachine : MonoBehaviour, ISaveLoadObject
    {
        [SerializeField] private Smartphone _smartphone;
        [SerializeField] private AudioClip _freePlaySound;

        private SaveLoadServise _saveLoadServise;

        private BaseState _currentState;
        private List<BaseState> _allStates;

        private string _saveKay;

        public BaseState CurrentState => _currentState;

        public event Action StateChanged;

        [Inject]
        public void Construct(SaveLoadServise saveLoadServise, AudioServise audioServise)
        {
            _saveLoadServise = saveLoadServise;

            _allStates = new List<BaseState>()
            {
                new StoryState(_smartphone),
                new PlayState(audioServise, _freePlaySound)
            };

            ChangeState<StoryState>();

            if (saveLoadServise.HasSave(_saveKay))
                Load();
        }

        private void OnDestroy()
        {
            Save();
        }

        public void ChangeState<T>() where T : BaseState
        {
            if (_currentState != null)
                _currentState.Exit();

            BaseState newState = _allStates.FirstOrDefault(state => state is T);
            newState.Enter();

            _currentState = newState;
            StateChanged?.Invoke();
        }

        public void Save()
        {
            _saveLoadServise.Save(_saveKay, new SaveData.IntData() { Int = _allStates.FindIndex(state => state == _currentState) });
        }

        public void Load()
        {
            BaseState lastState = _allStates.ElementAt(_saveLoadServise.Load<SaveData.IntData>(_saveKay).Int);
            lastState.Enter();

            _currentState = lastState;
        }
    }    
}
