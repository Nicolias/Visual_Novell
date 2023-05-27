using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoisePanel : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private ChoiceButton _choiseButtonTemplate;
    [SerializeField] private TMP_Text _questionText;

    public void Show(string question, List<ChoiseElement> choiseElements)
    {
        for (int i = 0; i < _container.childCount; i++)
            Destroy(_container.GetChild(i).gameObject);

        _questionText.text = question;

        for (int i = 0; i < choiseElements.Count; i++)
        {
            ChoiceButton choiceButton = Instantiate(_choiseButtonTemplate, _container);

            choiceButton.Initialized(choiseElements[i]);
        }
    }
}