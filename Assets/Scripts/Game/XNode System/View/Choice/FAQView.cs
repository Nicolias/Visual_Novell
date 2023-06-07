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
        _selfCanvas.enabled = true;
        for (int i = 0; i < questions.Count; i++)
        {
            ChoiceButton choiceButton = Instantiate(_choiseButtonTemplate, _container);

            ChoiseElement choiseElement = GetChoiceElement(questions[i]);

            choiceButton.Initialized(choiseElement);
        }
    }

    private ChoiseElement GetChoiceElement((Node, string) question)
    {
        return new(question.Item2, () =>
        {
            OnQuestionSelected?.Invoke(question);
            HideCanvas();
        });
    }

    private void HideCanvas()
    {
        _selfCanvas.enabled = false;

        foreach (Transform item in _container)
            Destroy(item.gameObject);
    }
}