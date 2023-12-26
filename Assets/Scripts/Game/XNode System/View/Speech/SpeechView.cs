using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public abstract class SpeechView : MonoBehaviour, ISpeechView
{
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

    public event Action Clicked;

    private void Awake()
    {
        _waitForSeconds = new(_timeBetweenShowNewCharInDialog);
        _stringBuilder = new();
    }

    private void OnEnable()
    {
        _clickerZone.Clicked += OnCallBack;

        StartCoroutine(ShowEnumerators());
    }

    private void OnDisable()
    {
        _clickerZone.Clicked -= OnCallBack;

        StopCoroutine(ShowEnumerators());
        _enumerators.Clear();
    }

    public void ShowSmooth(ISpeechModel model)
    {
        _currentText = model.Text;

        if (gameObject.activeInHierarchy)
            StartCoroutine(ShowingSpeechSmooth());
        else
            _enumerators.Add(ShowingSpeechSmooth());

    }

    public void Show(ISpeechModel speechModel)
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

        Clicked?.Invoke();
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