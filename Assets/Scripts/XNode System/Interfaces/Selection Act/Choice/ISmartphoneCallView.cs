using System;
using XNode;

public interface ISmartphoneCallView
{
    public event Action<Node> OnChoiceMade;
    public void Show(ICallModel model);
}