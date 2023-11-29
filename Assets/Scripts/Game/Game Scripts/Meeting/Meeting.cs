using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Meeting : MonoBehaviour
{
    [SerializeField] private Eating _eating;

    [SerializeField] private CharacterRenderer _characterRenderer;
    [SerializeField] private StaticData _staticData;
    [SerializeField] private Smartphone _smartphone;
    [SerializeField] private DialogSpeechView _dialogSpeechView;

    private ChoicePanel _choicePanel;
    private CharactersLibrary _charactersLibrary;

    private PastimeSelectionFactory _pastimeSelectionFactory;
    private AbstractPastime _currentPastime;  
    private CharacterView _characterView;

    private DialogSpeechPresenter _dialogSpeechPresenter;

    public event UnityAction Ended;

    [Inject]
    public void Construct(MiniGameSelector miniGameSelector, Quiz quiz, ChoicePanel choicePanel, CharactersLibrary charactersLibrary)
    {
        _choicePanel = choicePanel;
        _charactersLibrary = charactersLibrary;

        _pastimeSelectionFactory = new PastimeSelectionFactory(choicePanel,
            new Dictionary<PastimeOnLocationType, AbstractPastime>()
            {
                { PastimeOnLocationType.Quiz, new QuizPastime(quiz, choicePanel) },
                { PastimeOnLocationType.MiniGame, new MiniGamePastime(miniGameSelector, choicePanel) },
                {PastimeOnLocationType.Cafe, new EatingPastime(_eating, _choicePanel, _charactersLibrary) }
            });
    }

    public void Enter(CharacterView characterView)
    {
        _characterView = characterView;
        characterView.SetInteractable(false);

        _pastimeSelectionFactory.PastimeSelected += OnPastimeSelected;
        _pastimeSelectionFactory.EndMeetingSelected += OnEndMeetingSelected;

        _pastimeSelectionFactory.Show(characterView.AvailablePastimes);

        _dialogSpeechPresenter = null;
    }

    private void OnPastimeSelected(AbstractPastime pastime)
    {
        pastime.Enter(_characterView.Type);
        pastime.Ended += OnCurrentPastimeEnded;

        _currentPastime = pastime;
    }

    private void OnCurrentPastimeEnded()
    {
        _currentPastime.Ended -= OnCurrentPastimeEnded;

        _pastimeSelectionFactory.Show(_characterView.AvailablePastimes);
    }

    private void OnEndMeetingSelected()
    {
        if (_currentPastime != null)
            _currentPastime.Ended -= OnCurrentPastimeEnded;

        _pastimeSelectionFactory.PastimeSelected -= OnPastimeSelected;
        _pastimeSelectionFactory.EndMeetingSelected -= OnEndMeetingSelected;

        Exit();
    }

    private void Exit()
    {
        _choicePanel.Hide();

        _dialogSpeechPresenter = new DialogSpeechPresenter(_charactersLibrary.GetCharacter(_characterView.Type).ScriptableObject.DialogAfterMeeting, _dialogSpeechView, _staticData);

        _dialogSpeechPresenter.Complete += DialogAfterMeetingCompleted;
        _dialogSpeechPresenter.Execute();
    }

    private void DialogAfterMeetingCompleted()
    {
        _dialogSpeechPresenter.Complete -= DialogAfterMeetingCompleted;

        _dialogSpeechView.Hide();
        _characterRenderer.Delete(_characterView.Data);

        Ended?.Invoke();
    }
}
