using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoicePanel : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private ChoiceButton _choiseButtonTemplate;
    [SerializeField] private TMP_Text _questionText;

    public List<ChoiceButton> ShowAndGetUncorrectButtons(string question, List<(AnswerType, ChoiseElement)> choiseElements)
    {
        gameObject.SetActive(true);
        _questionText.text = question;

        ClearContainer();

        List<ChoiceButton> uncorrectButtons = new List<ChoiceButton>();

        for (int i = 0; i < choiseElements.Count; i++)
        {
            ChoiceButton choiceButton = Instantiate(_choiseButtonTemplate, _container);

            choiceButton.Initialized(choiseElements[i].Item2);

            if(choiseElements[i].Item1 == AnswerType.Uncorrect)
                uncorrectButtons.Add(choiceButton);
        }

        return uncorrectButtons;
    }

    public void Show(string question, IEnumerable<ChoiseElement> choiseElements)
    {
        gameObject.SetActive(true);
        _questionText.text = question;

        ClearContainer();

        foreach (var choiseElement in choiseElements)
        {
            ChoiceButton choiceButton = Instantiate(_choiseButtonTemplate, _container);

            choiceButton.Initialized(choiseElement);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ClearContainer()
    {
        for (int i = 0; i < _container.childCount; i++)
            Destroy(_container.GetChild(i).gameObject);
    }
}