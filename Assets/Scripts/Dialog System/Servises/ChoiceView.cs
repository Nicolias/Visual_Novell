using System;
using TMPro;
using UnityEngine;
using XNode;

public class ChoiceView : MonoBehaviour, IChoiceView
{
    public event Action<Node> OnChoiceMade;

    [SerializeField] private Canvas _selfCanvas;
    [SerializeField] private Transform _container;
    [SerializeField] private ChoiceButton _choiseButtonTemplate;
    [SerializeField] private TMP_Text _questionText;

    public Canvas Canvas => _selfCanvas;

    private void Start()
    {
        HideCanvas();
    }

    public void Show(string[] texts, Node[] nodes, string questionText)
    {
        ShowCanvas();

        _questionText.text = questionText;

        for (int i = 0; i < nodes.Length; i++)
        {
            ChoiceButton choiceButton = Instantiate(_choiseButtonTemplate, _container);
            ChoiseElement choiseElement = new(texts[i], nodes[i]);

            choiceButton.Initialized(choiseElement);

            choiceButton.Button.onClick.AddListener(() =>
            {
                OnChoiceMade?.Invoke(choiceButton.Node);
                HideCanvas();
            });
        }
    }

    private void ShowCanvas() => _selfCanvas.gameObject.SetActive(true);

    private void HideCanvas()
    {
        _selfCanvas.gameObject.SetActive(false);

        for (int i = 0; i < _container.childCount; i++)
            Destroy(_container.GetChild(i).gameObject);
    }
}
