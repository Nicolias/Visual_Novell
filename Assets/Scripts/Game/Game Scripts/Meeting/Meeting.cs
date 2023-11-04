using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Meeting : MonoBehaviour
{
    [SerializeField] private StaticData _staticData;
    [SerializeField] private Smartphone _smartphone;
    [SerializeField] private DialogSpeechView _dialogSpeechView;

    private ChoicePanel _choicePanel;
    private CharactersLibrary _charactersLibrary;

    private PastimeSelectionFactory _pastimeSelectionFactory;
    private AbstractPastime _currentPastime;  
    private CharacterViewForMeeting _characterView;

    private DialogSpeechPresenter _dialogSpeechPresenter;

    [Inject]
    public void Construct(MiniGameSelector miniGameSelector, Quiz quiz, ChoicePanel choicePanel, CharactersLibrary charactersLibrary)
    {
        _choicePanel = choicePanel;
        _charactersLibrary = charactersLibrary;

        _pastimeSelectionFactory = new PastimeSelectionFactory(this, choicePanel,
            new Dictionary<PastimeOnLocationType, AbstractPastime>()
            {
                { PastimeOnLocationType.Quiz, new QuizPastime(quiz, choicePanel) },
                { PastimeOnLocationType.MiniGame, new MiniGamePastime(miniGameSelector, choicePanel) }
            });
    }

    public void Enter(CharacterViewForMeeting characterView)
    {
        ChangeEnableInSmartphone(false);

        _characterView = characterView;
        characterView.SetInteractable(false);

        _pastimeSelectionFactory.Show(characterView.AvailablePastimes);
        _pastimeSelectionFactory.PastimeSelected += OnPastimeSelected;

        _dialogSpeechPresenter = null;
    }

    public void Exit()
    {
        if (_currentPastime != null)
            _currentPastime.Ended -= OnPastimeEnded;

        _pastimeSelectionFactory.PastimeSelected -= OnPastimeSelected;
        _choicePanel.Hide();

        _dialogSpeechPresenter = new DialogSpeechPresenter(_charactersLibrary.GetCharacter(_characterView.Type).DialogAfterMeeting, _dialogSpeechView, _staticData);
        _dialogSpeechPresenter.Complete += DialogAfterMeetingCompleted;
        _dialogSpeechPresenter.Execute();
    }

    private void ChangeEnableInSmartphone(bool isEnabled)
    {
        _smartphone.ChangeEnabled(new List<(SmartphoneWindows, bool)>()
        {
            (SmartphoneWindows.DUX, isEnabled),
            (SmartphoneWindows.Map, isEnabled)
        });
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

    private void DialogAfterMeetingCompleted()
    {
        _dialogSpeechPresenter.Complete -= DialogAfterMeetingCompleted;

        _dialogSpeechView.Hide();
        Destroy(_characterView.gameObject);
        ChangeEnableInSmartphone(true);
        _characterView.SetInteractable(true);
    }
}
