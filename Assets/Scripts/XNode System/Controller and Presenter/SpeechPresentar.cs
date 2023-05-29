using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class SpeechPresentar : IPresentar 
{
    public event Action OnComplete;

    protected ISpeechModel _model;
    protected SpeechView _view;
    private StaticData _staticData;

    public SpeechPresentar(ISpeechModel model, SpeechView view, StaticData staticData)
    {
        _model = model;
        _view = view;
        _staticData = staticData;
    }    

    public void Execute()
    {
        Debug.Log("Sub");
        _view.OnClick += OnCallBackView;

        if (_view.gameObject.activeInHierarchy)
        {
            _view.ShowSmooth(_model);

            if (_model.IsImmediatelyNextNode)
                OnComplete?.Invoke();
        }
        else
        {
            _staticData.StartCoroutine(WaitUntilAndInvoke(() =>
            {
                _view.ShowSmooth(_model);

                if (_model.IsImmediatelyNextNode)
                    OnComplete?.Invoke();
            }));
        }
    }

    private void OnCallBackView()
    {
        if (_view.ShowStatus == ShowTextStatus.Showing)
        {
            if (_view.gameObject.activeInHierarchy)
            {
                _view.Show(_model);
                return;
            }
            else
            {
                _staticData.StartCoroutine(WaitUntilAndInvoke(() =>
                {
                    _view.Show(_model);
                    return;
                }));
            }
        }

        Debug.Log("Unsub");
        _view.OnClick -= OnCallBackView;
        OnComplete?.Invoke();
    }

    private IEnumerator WaitUntilAndInvoke(Action action)
    {
        yield return new WaitUntil(() => _view.gameObject.activeInHierarchy == true);
        action.Invoke();
    }
}
