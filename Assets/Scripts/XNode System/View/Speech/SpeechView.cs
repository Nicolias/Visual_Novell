using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SpeechView : MonoBehaviour, ISpeechView
{
    public event Action OnClick;

    [SerializeField] private Canvas _selfCanvas;
    [SerializeField] private Button _changeTextButton;

    [SerializeField] private float _timeBetweenShowNewCharInDialog;
    [SerializeField] private TMP_Text _speechText, _speakerName;
    [SerializeField] private Image _speakerAvatar;

    [SerializeField] private StaticData _staticData;

    private ShowTextStatus _showStatus;
    private WaitForSeconds _waitForSeconds;
    private StringBuilder _stringBuilder;

    public ShowTextStatus ShowStatus => _showStatus;


    [Inject]
    public void Construct(StaticData staticData)
    {
        _staticData = staticData;
    }

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

        _speakerName.text = speakerName.Replace(_staticData.SpecWordForNickName, _staticData.Nickname);
        _speechText.text = speechText.Replace(_staticData.SpecWordForNickName, _staticData.Nickname); ;

        if (speakerAvatar != null)
        {
            _speakerAvatar.sprite = speakerAvatar;
            _speakerAvatar.color = new(1, 1, 1, 1);
        }
        else
        {
            _speakerAvatar.color = new(1, 1, 1, 0);
        }

        _selfCanvas.gameObject.SetActive(true);
    }

    public void ShowSmooth(string speakerName, string speechText, Sprite speakerAvatar)
    {
        _speakerName.text = speakerName.Replace(_staticData.SpecWordForNickName, _staticData.Nickname);

        if (speakerAvatar != null)
        {
            _speakerAvatar.sprite = speakerAvatar;
            _speakerAvatar.color = new(1, 1, 1, 1);
        }
        else
        {
            _speakerAvatar.color = new(1, 1, 1, 0);
        }

        StartCoroutine(ShowingSpeechSmooth(speechText.Replace(_staticData.SpecWordForNickName, _staticData.Nickname)));

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
