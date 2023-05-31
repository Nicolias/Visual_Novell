using System;
using System.Collections;
using UnityEngine;

public class CoroutineServise : MonoBehaviour
{
    public Coroutine StartRoutine(IEnumerator method)
    {
        return StartCoroutine(method);
    }

    public void StopRoutine(IEnumerator method)
    {
        StopCoroutine(method);
    }

    public void WaitForSecondsAndInvoke(float seconds, Action action)
    {
        if (seconds <= 0) throw new InvalidOperationException("Нулевое время ожидания");

        StartCoroutine(IEnumeratorWaitForSecondsAndInvoke(action, seconds));
    }

    private IEnumerator IEnumeratorWaitForSecondsAndInvoke(Action action, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        action?.Invoke();
    }
}