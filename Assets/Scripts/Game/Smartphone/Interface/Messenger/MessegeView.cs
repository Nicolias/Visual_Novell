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

    public MessegeSenderType Sender { get; private set; }

    public void Show(Messege messege)
    {
        gameObject.SetActive(true);

        _avatar.sprite = messege.Avatar;
        _name.text = messege.Name;
        _messege.text = messege.Text;
        Sender = messege.SenderType;

        StartCoroutine(WaitFrameAndInvoke(() => 
            _selfRect.sizeDelta = new Vector2(_selfRect.sizeDelta.x, _messege.rectTransform.sizeDelta.y + _sizeOffset)));
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator WaitFrameAndInvoke(Action action)
    {
        yield return new WaitForEndOfFrame();
        action?.Invoke();
    }
}
