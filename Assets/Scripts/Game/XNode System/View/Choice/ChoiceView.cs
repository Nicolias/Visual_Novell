using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ChoiceView : MonoBehaviour, IChoiceView
{
    public virtual event Action<Node> OnChoiceMade;

    [SerializeField] private ChoicePanel _choisePanel;

    public void Show(IChoiceModel model)
    {
        List<ChoiseElement> choiseElements = new();

        for (int i = 0; i < model.Nodes.Length; i++)
            choiseElements.Add(GetChoiceElement((model.Nodes[i], model.Choices[i])));

        _choisePanel.Show(model.QuestionText, choiseElements);
    }

    private ChoiseElement GetChoiceElement((Node, string) model)
    {
        return new(model.Item2, () =>
        {
            OnChoiceMade?.Invoke(model.Item1);
            _choisePanel.Hide();
        });
    }
}
