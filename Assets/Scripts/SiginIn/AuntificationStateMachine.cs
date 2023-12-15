using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;
using System.Collections.Generic;
using System.Linq;

namespace Auntification.StateMachine
{
    public class AuntificationStateMachine : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _userName;
        [SerializeField] private TMP_InputField _password;

        [SerializeField] private Button _auntificaitonButton;
        [SerializeField] private TMP_Text _auntificationButtonText;
        [SerializeField] private Button _changeStateButton;
        [SerializeField] private TMP_Text _changeStateButtonText;

        private readonly List<AbstractState> _states = new List<AbstractState>();
        private AbstractState _currentState;

        private AuntificationServise _auntificationServise;

        [Inject]
        public void Construct(AuntificationServise auntificationServise)
        {
            _auntificationServise = auntificationServise;

            auntificationServise.Initialize(_userName, _password);
        }

        private void Awake()
        {
            _states.Add(new LogInState(_auntificaitonButton, _auntificationButtonText, _changeStateButtonText, _auntificationServise));
            _states.Add(new SignUpState(_auntificaitonButton, _auntificationButtonText, _changeStateButtonText, _auntificationServise));

            SwitchState();
        }

        private void OnEnable()
        {
            _changeStateButton.onClick.AddListener(SwitchState);
        }

        private void OnDisable()
        {
            _changeStateButton.onClick.RemoveListener(SwitchState);
        }

        private void SwitchState()
        {
            if (_currentState != null)
                _currentState.Exite();

            _currentState = _states.FirstOrDefault(state => state != _currentState);
            _currentState.Enter();

            _userName.text = "";
            _password.text = "";
        }
    }
}