using System;

public class ShowInterstaionCommand : ICommand
{
    private readonly AdsServise _adsServise;

    public ShowInterstaionCommand(AdsServise adsServise)
    {
        _adsServise = adsServise;
    }

    public event Action Completed;

    public void Execute()
    {
        _adsServise.OnShowInterstitialButtonClick();
        _adsServise.AdsShowed += OnShowed;
    }

    private void OnShowed()
    {
        _adsServise.AdsShowed -= OnShowed;
        Completed?.Invoke();
    }
}