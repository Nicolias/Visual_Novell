using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessegeView : MonoBehaviour
{
    [SerializeField] private Image _avatar;
    [SerializeField] private TMP_Text _name, _messege;

    [SerializeField] private RectTransform _selfRect;

    [SerializeField] private float _sizeOffset;

    public void Initialize(Messege messege)
    {
        _avatar.sprite = messege.Avatar;
        _name.text = messege.Name;
        _messege.text = messege.MessegeText;

        StartCoroutine(WaitFrameAndInvoke(() => 
            _selfRect.sizeDelta = new Vector2(_selfRect.sizeDelta.x, _messege.rectTransform.sizeDelta.y + _sizeOffset)));
    }


    private IEnumerator WaitFrameAndInvoke(Action action)
    {
        yield return new WaitForEndOfFrame();
        action?.Invoke();
    }
}
