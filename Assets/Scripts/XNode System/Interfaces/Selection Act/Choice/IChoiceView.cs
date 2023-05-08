using System;
using XNode;

public interface IChoiceView
{
    public event Action<Node> OnChoiceMade;
    public void Show(IChoiceModel model);
}
