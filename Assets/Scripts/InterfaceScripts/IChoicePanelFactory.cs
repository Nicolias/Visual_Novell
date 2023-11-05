using UnityEngine;

public interface IChoicePanelFactory
{
    public ChoicePanel CreateChoicePanel(Transform container);
}