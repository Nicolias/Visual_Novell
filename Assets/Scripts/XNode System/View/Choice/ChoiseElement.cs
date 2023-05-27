using System;
using TMPro;
using XNode;

public class ChoiseElement
{
    public string Text { get; private set; }

    public Action ActionWhenOnClick { get; private set; }

    public ChoiseElement(string text, Action actionWhenOnClick)
    {
        Text = text;
        ActionWhenOnClick = actionWhenOnClick;
    }
}