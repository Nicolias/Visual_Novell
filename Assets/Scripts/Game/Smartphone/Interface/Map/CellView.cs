using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CellView : MonoBehaviour
{
    private TMP_Text _textOnCell;
    private Button _selfButton;

    public event Action Clicked;

    private void Awake()
    {
        _textOnCell = GetComponentInChildren<TMP_Text>();
        _selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(() => Clicked?.Invoke());
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
    }

    public void Initialize(string textOnCell)
    {
        _textOnCell.text = textOnCell;
    }

    public void Destory()
    {
        DestroyImmediate(gameObject);
    }
}
