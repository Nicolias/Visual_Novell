using System;

public class SmartPhoneGuidPresenter : IPresentar
{
    public event Action Complete;

    private SmartphoneGuideView _smartphoneGuideView;

    public SmartPhoneGuidPresenter(SmartphoneGuideView smartphoneGuideView)
    {
        _smartphoneGuideView = smartphoneGuideView;
    }

    public void Execute()
    {
        _smartphoneGuideView.Show();
        _smartphoneGuideView.OnComplete += CallBack;
    }

    private void CallBack()
    {
        _smartphoneGuideView.OnComplete -= CallBack;
        Complete?.Invoke();
    }
}
