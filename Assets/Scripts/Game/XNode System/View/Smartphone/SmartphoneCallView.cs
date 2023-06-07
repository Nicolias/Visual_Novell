using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class SmartphoneCallView : MonoBehaviour, ISmartphoneCallView
{
    public virtual event Action<Node> OnChoiceMade;

    [SerializeField] private Canvas _selfCanvas;
    [SerializeField] private Transform _container;
    [SerializeField] private Button _choiseButtonTemplate;
    [SerializeField] private TMP_Text _characterName;

    public Canvas Canvas => _selfCanvas;

    public void Show(ICallModel model)
    {
        ShowCanvas();

        _characterName.text = model.CharacterName;

        for (int i = 0; i < model.Nodes.Length; i++)
        {
            Button choiceButton = Instantiate(_choiseButtonTemplate, _container);
            choiceButton.image.sprite = model.Images[i];

            AssignEventOnButton(choiceButton, model.Nodes[i]);
        }
    }

    private void AssignEventOnButton(Button choiceButton, Node node)
    {
        choiceButton.onClick.AddListener(() =>
        {
            HideCanvas();
            OnChoiceMade?.Invoke(node);
            choiceButton.onClick.RemoveAllListeners();
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