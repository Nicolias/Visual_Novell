using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoicePanel : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private ChoiceButton _choiseButtonTemplate;
    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private Canvas _selfCanvas;

    public List<ChoiceButton> ShowAndGetUncorrectButtons(string question, List<(AnswerType, ChoiseElement)> choiseElements)
    {
        _selfCanvas.enabled = true;
        _questionText.text = question;

        ClearContainer();

        List<ChoiceButton> uncorrectButtons = new();

        for (int i = 0; i < choiseElements.Count; i++)
        {
            ChoiceButton choiceButton = Instantiate(_choiseButtonTemplate, _container);

            choiceButton.Initialized(choiseElements[i].Item2);

            if(choiseElements[i].Item1 == AnswerType.Uncorrect)
                uncorrectButtons.Add(choiceButton);
        }

        return uncorrectButtons;
    }

    internal void Show(string v, object getChoiceElements)
    {
        throw new NotImplementedException();
    }

    public void Show(string question, List<ChoiseElement> choiseElements)
    {
        _selfCanvas.enabled = true;
        _questionText.text = question;

        ClearContainer();

        for (int i = 0; i < choiseElements.Count; i++)
        {
            ChoiceButton choiceButton = Instantiate(_choiseButtonTemplate, _container);

            choiceButton.Initialized(choiseElements[i]);
        }
    }

    public void Hide()
    {
        _selfCanvas.enabled = false;
    }

    private void ClearContainer()
    {
        for (int i = 0; i < _container.childCount; i++)
            Destroy(_container.GetChild(i).gameObject);
    }
}