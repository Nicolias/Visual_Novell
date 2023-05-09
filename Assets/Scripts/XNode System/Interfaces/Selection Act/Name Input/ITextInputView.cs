using System;
using XNode;

public interface ITextInputView
{
    public event Action<string> OnTextInput;

    public void Show();
}
