using System;

public class ChoiseElement
{
    public string TextOnButton { get; private set; }

    public Action ActionWhenOnClick { get; private set; }

    public ChoiseElement(string textOnButton, Action actionWhenOnClick)
    {
        TextOnButton = textOnButton;
        ActionWhenOnClick = actionWhenOnClick;
    }
}