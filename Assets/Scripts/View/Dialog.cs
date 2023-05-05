using System;
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

    [SerializeField] private Dialogs _dialogs;
    [SerializeField] private TMP_Text _speakerName, _speechText;
    [SerializeField] private float _timeBetweenShowNewCharInDialog;
    [SerializeField] public Choise _choise;

    private SpeachTextStatus _currentShowStatus;

    private Speech _currentSpeech;

    private void OnEnable()
    {
        if (_dialogs != null)
            PlayNextSpeech();

        _changeTextButton.onClick.AddListener(() =>
        {
            if (_choise.Canvas.enabled == true)
                return;

            if (_currentShowStatus == SpeachTextStatus.Complete)
            {
                PlayNextSpeech();
            }
            else
            {
                StopAllCoroutines();
                _speechText.text = _currentSpeech.Text;
                _currentShowStatus = SpeachTextStatus.Complete;

                if(_currentSpeech.Choise.Length != 0)
                    CreateChoise(_currentSpeech);
            }
        });
    }

    private void OnDisable()
    {
        _changeTextButton.onClick.RemoveAllListeners();
    }

    public void Choise(ChoiseButton choiseButton)
    {
        _currentSpeech = (Speech)choiseButton.Node;
        choiseButton.Hide();
        _choise.Hide();

        PlayNextSpeech();
    }

    private void PlayNextSpeech()
    {
        if (_dialogs == null)
            return;

        _currentSpeech ??= (Speech)_dialogs.nodes[0];

        _speakerName.text = _currentSpeech.SpeakerName;
        StartCoroutine(ShowingSpeech(_currentSpeech.Text));

        if (_currentSpeech.Choise.Length == 0)
        {
            NodePort nodePort = _currentSpeech.GetPort("_outPut").Connection;

            if (nodePort != null) 
                _currentSpeech = (Speech)nodePort.node;
        }
        else
        {
            CreateChoise(_currentSpeech);
        }
    }

    private IEnumerator ShowingSpeech(string speechText)
    {
        StringBuilder stringBuilder = new();

        _currentShowStatus = SpeachTextStatus.Showing;

        foreach (char charInDialog in speechText)
        {
            yield return new WaitForSeconds(_timeBetweenShowNewCharInDialog);
            stringBuilder.Append(charInDialog);

            _speechText.text = stringBuilder.ToString();
        }

        _currentShowStatus = SpeachTextStatus.Complete;
    }

    private void CreateChoise(Speech speech)
    {
        _choise.Show();

        for (int i = 0; i < speech.Choise.Length; i++)
        {
            ChoiseElement choiseElement = new();
            choiseElement.Text = speech.Choise[i];
            choiseElement.Node = speech.GetPort($"_choise {i}").Connection.node;
            _choise.Add(choiseElement, this);
        }
    }
}
