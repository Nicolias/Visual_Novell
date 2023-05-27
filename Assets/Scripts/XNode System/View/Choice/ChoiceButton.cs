using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _selfButton;

    public Action ActionWhenOnClick { get; private set; }
    public string ChoiceText => _text.text;
    public Button Button => _selfButton;

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(() => ActionWhenOnClick.Invoke());
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
    }

    public void Initialized(ChoiseElement choiseElement)
    {
        ActionWhenOnClick = choiseElement.ActionWhenOnClick;
        _text.text = choiseElement.Text;
    }
}