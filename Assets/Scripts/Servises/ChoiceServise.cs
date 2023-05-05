using System;
using UnityEngine;

public class ChoiceServise : MonoBehaviour
{
    public event Action<Speech> OnChoiceMade;

    [SerializeField] private Canvas _selfCanvas;
    [SerializeField] private Transform _container;
    [SerializeField] private ChoiceButton _choiseButtonTemplate;

    public Canvas Canvas => _selfCanvas;

    private void Start()
    {
        _selfCanvas.enabled = false;   
    }

    public void CreateChoiseButton(ChoiseElement choiseElement, Dialog currentDialog)
    {
        ShowCanvas();
       
        ChoiceButton choiceButton = Instantiate(_choiseButtonTemplate, _container);
        choiceButton.Initialized(currentDialog, choiseElement);

        choiceButton.Button.onClick.AddListener(() =>
        {
            OnChoiceMade?.Invoke((Speech)choiceButton.Node);
            HideCanvas();
        });
    }

    private void ShowCanvas() => _selfCanvas.enabled = true;

    private void HideCanvas()
    {
        _selfCanvas.enabled = false;

        for (int i = 0; i < _container.childCount; i++)
            Destroy(_container.GetChild(i).gameObject);
    }
}
