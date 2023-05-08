using System;
using XNode;

public interface ITextInputView
{
    public event Action<string> OnTextInput;
    public event Action<Node> OnChoiceMade;

    public void Show();
}
