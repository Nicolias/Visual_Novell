using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using XNode;

public class FAQView : MonoBehaviour
{
    public event Action<(Node, string)> OnQuestionSelected;

    [SerializeField] private Canvas _selfCanvas;
    [SerializeField] private Transform _container;
    [SerializeField] private ChoiceButton _choiseButtonTemplate;

    public Canvas Canvas => _selfCanvas;

    public void Show(List<(Node, string)> questions)
    {
        ShowCanvas();

        for (int i = 0; i < questions.Count; i++)
        {
            ChoiceButton choiceButton = Instantiate(_choiseButtonTemplate, _container);
            ChoiseElement choiseElement = new(questions[i].Item2, questions[i].Item1);

            choiceButton.Initialized(choiseElement);

            AssignEventOnButton(choiceButton);
        }
    }

    private void AssignEventOnButton(ChoiceButton choiceButton)
    {
        choiceButton.Button.onClick.AddListener(() =>
        {
            OnQuestionSelected?.Invoke((choiceButton.Node, choiceButton.ChoiceText));
            HideCanvas();
        });
    }

    private void ShowCanvas() => _selfCanvas.gameObject.SetActive(true);

    private void HideCanvas()
    {
        _selfCanvas.gameObject.SetActive(false);

        for (int i = 0; i < _container.childCount; i++)
            Destroy(_container.GetChild(i).gameObject);
    }
}