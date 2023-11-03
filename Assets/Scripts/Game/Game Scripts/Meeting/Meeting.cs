using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Meeting : MonoBehaviour
{
    private PastimeSelectionFactory _pastimeSelectionFactory;
    private ChoicePanel _choicePanel;

    private CharacterView _characterView;
    private AbstractPastime _currentPastime;  

    [Inject]
    public void Construct(MiniGameSelector miniGameSelector, Quiz quiz, ChoicePanel choicePanel)
    {
        _choicePanel = choicePanel;

        _pastimeSelectionFactory = new PastimeSelectionFactory(this, choicePanel,
            new Dictionary<PastimeOnLocationType, AbstractPastime>()
            {
                { PastimeOnLocationType.Quiz, new QuizPastime(quiz, choicePanel) },
                { PastimeOnLocationType.MiniGame, new MiniGamePastime(miniGameSelector, choicePanel) }
            });
    }

    public void Enter(CharacterView characterView)
    {
        _characterView = characterView;
        characterView.SetInteractable(false);

        _pastimeSelectionFactory.Show(characterView.AvailablePastimes);
        _pastimeSelectionFactory.PastimeSelected += OnPastimeSelected;
    }

    public void Exit()
    {
        if (_currentPastime != null)
            _currentPastime.Ended -= OnPastimeEnded;

        _pastimeSelectionFactory.PastimeSelected -= OnPastimeSelected;
        _characterView.SetInteractable(true);
        _choicePanel.Hide();
    }

    private void OnPastimeSelected(AbstractPastime pastime)
    {
        pastime.Enter(_characterView.Type);
        pastime.Ended += OnPastimeEnded;

        _currentPastime = pastime;
    }

    private void OnPastimeEnded()
    {
        _currentPastime.Ended -= OnPastimeEnded;

        _pastimeSelectionFactory.Show(_characterView.AvailablePastimes);
    }
}