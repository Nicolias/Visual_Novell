using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechView : MonoBehaviour, ISpeechView
{
    public event Action OnClick;

    [SerializeField] public Canvas _selfCanvas;
    [SerializeField] private Button _changeTextButton;

    [SerializeField] private float _timeBetweenShowNewCharInDialog;
    [SerializeField] private TMP_Text _speechText, _speackerName;

    private ShowTextStatus _showStatus;
    private WaitForSeconds _waitForSeconds;
    private StringBuilder _stringBuilder;

    public ShowTextStatus ShowStatus => _showStatus;

    private void Awake()
    {
        _waitForSeconds = new(_timeBetweenShowNewCharInDialog);
        _stringBuilder = new();
    }

    private void OnEnable()
    {
        //if (_dialogs != null)
        //{
        //    _currentSpeech = (SpeechModel)_dialogs.nodes[0];
        //    //_speechWriter.DisplaySpeechSmooth(_currentSpeech);
        //}

        _changeTextButton.onClick.AddListener(OnCallBack);

            //if (_choiseServise.Canvas.enabled == true)
            //    return;

            //if (_speechWriter.ShowStatus == SpeachTextStatus.Complete)
            //    TryDisplayNextSpeech();
            //else
            //    _speechWriter.DisplaySpeech(_currentSpeech);
           
            //if (_currentSpeech.Choise.Length != 0)
            //    CreateChoise(_currentSpeech);            
       // });

        //_choiseServise.OnChoiceMade += DisplayChoiseText;
    }

    private void OnDisable()
    {
        _changeTextButton.onClick.RemoveAllListeners();
       // _choiseServise.OnChoiceMade -= DisplayChoiseText;
    }

    //private void TryDisplayNextSpeech()
    //{
    //    NodePort nodePort = _currentSpeech.GetPort("_outPut").Connection;

    //    if (nodePort != null)
    //    {
    //        _currentSpeech = (SpeechModel)nodePort.node;

    //        _speechWriter.DisplaySpeechSmooth(_currentSpeech);
    //    }
    //}

    //private void CreateChoise(SpeechModel speech)
    //{
    //    for (int i = 0; i < speech.Choise.Length; i++)
    //    {
    //        ChoiseElement choiseElement = new(speech.Choise[i], speech.GetPort($"_choise {i}").Connection.node);
    //        _choiseServise.CreateChoiseButton(choiseElement, this);
    //    }        
    //}

    //private void DisplayChoiseText(SpeechModel speech)
    //{
    //    _currentSpeech = speech;

    //    _speechWriter.DisplaySpeechSmooth(speech);
    //}

    public void Show(string speakerName, string speechText)
    {
        _stringBuilder.Clear();
        _showStatus = ShowTextStatus.Complete;

        StopAllCoroutines();

        _speackerName.text = speakerName;
        _speechText.text = speechText;

        _selfCanvas.gameObject.SetActive(true);
    }

    public void ShowSmooth(string speakerName, string speechText)
    {
        _speackerName.text = speakerName;
        StartCoroutine(ShowingSpeechSmooth(speechText));

        _selfCanvas.gameObject.SetActive(true);
    }

    public void OnCallBack()
    {
        OnClick?.Invoke();
    }

    public void Hide()
    {
        _selfCanvas.gameObject.SetActive(false);
    }

    private IEnumerator ShowingSpeechSmooth(string speechText)
    {
        _showStatus = ShowTextStatus.Showing;

        foreach (char charInDialog in speechText)
        {
            yield return _waitForSeconds;
            _stringBuilder.Append(charInDialog);

            _speechText.text = _stringBuilder.ToString();
        }

        _showStatus = ShowTextStatus.Complete;
        _stringBuilder.Clear();
    }
}
