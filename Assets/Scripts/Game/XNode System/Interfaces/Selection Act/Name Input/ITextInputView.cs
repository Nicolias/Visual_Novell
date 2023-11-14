using System;
using XNode;

public interface ITextInputView
{
    public event Action<string> TextInput;

    public void Show();
}
