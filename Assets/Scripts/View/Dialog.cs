using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public enum SpeachTextStatus
{
    Showing,
    Complete
}

public class Dialog : MonoBehaviour
{
    [SerializeField] private Button _changeTextButton;

    [SerializeField] private SpeechWriter _speechWriter;
    [SerializeField] private ChoiceServise _choiseServise;
    [SerializeField] private DialogData _dialogs;
    
    private Speech _currentSpeech;

    private void OnEnable()
    {
        if (_dialogs != null)
        {
            _currentSpeech = (Speech)_dialogs.nodes[0];
            _speechWriter.DisplaySpeechSmooth(_currentSpeech);
        }

        _changeTextButton.onClick.AddListener(() =>
        {
            if (_choiseServise.Canvas.enabled == true)
                return;

            if (_speechWriter.ShowStatus == SpeachTextStatus.Complete)
                TryDisplayNextSpeech();
            else
                _speechWriter.DisplaySpeech(_currentSpeech);
           
            if (_currentSpeech.Choise.Length != 0)
                CreateChoise(_currentSpeech);            
        });

        _choiseServise.OnChoiceMade += DisplayChoiseText;
    }

    private void OnDisable()
    {
        _changeTextButton.onClick.RemoveAllListeners();
        _choiseServise.OnChoiceMade -= DisplayChoiseText;
    }

    private void TryDisplayNextSpeech()
    {
        NodePort nodePort = _currentSpeech.GetPort("_outPut").Connection;

        if (nodePort != null)
        {
            _currentSpeech = (Speech)nodePort.node;

            _speechWriter.DisplaySpeechSmooth(_currentSpeech);
        }
    }

    private void CreateChoise(Speech speech)
    {
        for (int i = 0; i < speech.Choise.Length; i++)
        {
            ChoiseElement choiseElement = new(speech.Choise[i], speech.GetPort($"_choise {i}").Connection.node);
            _choiseServise.CreateChoiseButton(choiseElement, this);
        }        
    }

    private void DisplayChoiseText(Speech speech)
    {
        _currentSpeech = speech;

        _speechWriter.DisplaySpeechSmooth(speech);
    }
}
