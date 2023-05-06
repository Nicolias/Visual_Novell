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
    [SerializeField] private TMP_Text _speechText, _speakerName;
    [SerializeField] public Image _speakerAvatar;

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
        _changeTextButton.onClick.AddListener(OnCallBack);
    }

    private void OnDisable()
    {
        _changeTextButton.onClick.RemoveAllListeners();
    }

    public void Show(string speakerName, string speechText, Sprite speakerAvatar)
    {
        _stringBuilder.Clear();
        _showStatus = ShowTextStatus.Complete;

        StopAllCoroutines();

        _speakerName.text = speakerName;
        _speechText.text = speechText;

        _speakerAvatar.sprite = speakerAvatar;

        _selfCanvas.gameObject.SetActive(true);
    }

    public void ShowSmooth(string speakerName, string speechText, Sprite speakerAvatar)
    {
        _speakerName.text = speakerName;
        _speakerAvatar.sprite = speakerAvatar;
        StartCoroutine(ShowingSpeechSmooth(speechText));

        _selfCanvas.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _selfCanvas.gameObject.SetActive(false);
    }

    private void OnCallBack()
    {
        OnClick?.Invoke();
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
