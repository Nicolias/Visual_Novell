using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public abstract class SpeechView : MonoBehaviour, ISpeechView
{
    public event Action OnClick;

    [SerializeField] private Canvas _selfCanvas;
    [SerializeField] private ClickerZone _clickerZone;

    [SerializeField] private float _timeBetweenShowNewCharInDialog;
    [SerializeField] private TMP_Text _speechText;

    private WaitForSeconds _waitForSeconds;

    private ShowTextStatus _showStatus;
    private StringBuilder _stringBuilder;

    private string _currentText;

    private List<IEnumerator> _enumerators = new List<IEnumerator>();

    public ShowTextStatus ShowStatus => _showStatus;

    private void Awake()
    {
        _waitForSeconds = new(_timeBetweenShowNewCharInDialog);
        _stringBuilder = new();
    }

    private void OnEnable()
    {
        _clickerZone.OnClick += OnCallBack;

        StartCoroutine(ShowEnumerators());
    }

    private void OnDisable()
    {
        _clickerZone.OnClick -= OnCallBack;

        StopCoroutine(ShowEnumerators());
        _enumerators.Clear();
    }

    public virtual void ShowSmooth(ISpeechModel model)
    {
        _currentText = model.Text;

        if (gameObject.activeInHierarchy)
            StartCoroutine(ShowingSpeechSmooth());
        else
            _enumerators.Add(ShowingSpeechSmooth());

    }

    public virtual void Show(ISpeechModel speechModel)
    {
        if (_currentText == null)
            throw new InvalidOperationException("Сначало нужно показывать текст плавно");

        StopAllCoroutines();

        _selfCanvas.enabled = true;
        _showStatus = ShowTextStatus.Complete;
        _stringBuilder.Clear();

        _speechText.text = _currentText;
    }

    public void Hide()
    {
        _selfCanvas.enabled = false;
    }

    private void OnCallBack()
    {
        if (ShowStatus == ShowTextStatus.Complete)
            Hide();

        OnClick?.Invoke();
    }

    private IEnumerator ShowingSpeechSmooth()
    {
        _selfCanvas.enabled = true;
        _showStatus = ShowTextStatus.Showing;

        foreach (char charInDialog in _currentText)
        {
            yield return _waitForSeconds;
            _stringBuilder.Append(charInDialog);

            _speechText.text = _stringBuilder.ToString();
        }

        _showStatus = ShowTextStatus.Complete;
        _stringBuilder.Clear();
    }

    private IEnumerator ShowEnumerators()
    {
        for (int i = 0; i < _enumerators.Count; i++)
            yield return _enumerators[i];

        _enumerators.Clear();
    }
}