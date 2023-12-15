using TMPro;
using UnityEngine.UI;
using System;

namespace Auntification.StateMachine
{
    public abstract class AbstractState
    {
        private TMP_Text _changeStateText;

        private Button _auntificationButton;
        private TMP_Text _auntificationButtonText;

        protected AbstractState(Button auntificationButton, TMP_Text auntificationButtonText, TMP_Text changeStateText, AuntificationServise auntificationServise)
        {
            _auntificationButton = auntificationButton;
            _auntificationButtonText = auntificationButtonText;
            _changeStateText = changeStateText;

            AuntificationServise = auntificationServise;
        }

        protected AuntificationServise AuntificationServise { get; private set; }

        public void Enter()
        {
            _auntificationButton.onClick.AddListener(Auntificat);
            _auntificationButtonText.text = GetAuntifiacationText();
            _changeStateText.text = GetChangeStateText();
        }

        public void Exite()
        {
            _auntificationButton.onClick.RemoveListener(Auntificat);
        }

        protected abstract string GetAuntifiacationText();

        protected abstract string GetChangeStateText();

        protected abstract void Auntificat();
    }
}