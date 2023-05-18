using System;
using TMPro;
using UnityEngine;
using XNode;

public class ChoiceView : MonoBehaviour, IChoiceView
{
    public virtual event Action<Node> OnChoiceMade;

    [SerializeField] private Canvas _selfCanvas;
    [SerializeField] private Transform _container;
    [SerializeField] private ChoiceButton _choiseButtonTemplate;
    [SerializeField] private TMP_Text _questionText;

    public Canvas Canvas => _selfCanvas;

    public void Show(IChoiceModel model)
    {
        ShowCanvas();

        _questionText.text = model.QuestionText;

        for (int i = 0; i < model.Nodes.Length; i++)
        {
            ChoiceButton choiceButton = Instantiate(_choiseButtonTemplate, _container);
            ChoiseElement choiseElement = new(model.Choices[i], model.Nodes[i]);

            choiceButton.Initialized(choiseElement);

            AssignEventOnButton(choiceButton);
        }
    }

    private void AssignEventOnButton(ChoiceButton choiceButton)
    {
        choiceButton.Button.onClick.AddListener(() =>
        {
            OnChoiceMade?.Invoke(choiceButton.Node);
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
