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
}