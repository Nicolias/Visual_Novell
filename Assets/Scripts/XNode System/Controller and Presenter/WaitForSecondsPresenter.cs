using System;
using System.Collections;
using UnityEngine;

public class WaitForSecondsPresenter : IPresentar
{
    public event Action OnComplete;

    private CoroutineServise _coroutineServise;
    private WaitForSecondsModel _model;

    public WaitForSecondsPresenter(CoroutineServise coroutineServise, WaitForSecondsModel model)
    {
        _coroutineServise = coroutineServise;
        _model = model;
    }

    public void Execute()
    {
        _coroutineServise.StartRoutine(WaitForSeconds());
    }

    private IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(_model.Seconds);

        OnComplete?.Invoke();
    }
}
