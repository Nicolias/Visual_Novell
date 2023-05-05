using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class SpeechWriter : MonoBehaviour
{
    [SerializeField] private float _timeBetweenShowNewCharInDialog;
    [SerializeField] private TMP_Text _speechText, _speackerName;

    private SpeachTextStatus _showStatus;
    private WaitForSeconds _waitForSeconds;
    private StringBuilder _stringBuilder;

    public SpeachTextStatus ShowStatus => _showStatus;

    private void Awake()
    {
        _waitForSeconds = new(_timeBetweenShowNewCharInDialog);
        _stringBuilder = new();
    }

    public void DisplaySpeechSmooth(Speech speech)
    {
        _speackerName.text = speech.SpeakerName;

        StartCoroutine(ShowingSpeechSmooth(speech.Text));
    }

    public void DisplaySpeech(Speech speech)
    {
        _stringBuilder.Clear();
        _showStatus = SpeachTextStatus.Complete;

        StopAllCoroutines();

        _speackerName.text = speech.SpeakerName;
        _speechText.text = speech.Text;
    }

    private IEnumerator ShowingSpeechSmooth(string speechText)
    {
        _showStatus = SpeachTextStatus.Showing;

        foreach (char charInDialog in speechText)
        {
            yield return _waitForSeconds;
            _stringBuilder.Append(charInDialog);

            _speechText.text = _stringBuilder.ToString();
        }

        _showStatus = SpeachTextStatus.Complete;
        _stringBuilder.Clear();
    }
}

