using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))] 
public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private Button _selfButton;

    public Action ActionWhenOnClick { get; private set; }
    public string ChoiceText => _text.text;
    public Button Button => _selfButton;

    private void Awake()
    {
        _selfButton = GetComponent<Button>();
    }

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
        _text.text = choiseElement.TextOnButton;
    }
}