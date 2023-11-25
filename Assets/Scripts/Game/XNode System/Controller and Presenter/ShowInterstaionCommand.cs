using System;

public class ShowInterstaionCommand : ICommand
{
    private readonly InterstionPanel _interstaialPanel;

    public ShowInterstaionCommand(InterstionPanel interstationPanel)
    {
        _interstaialPanel = interstationPanel;
    }

    public event Action Completed;

    public void Execute()
    {
        _interstaialPanel.ShowAds();
        _interstaialPanel.AdsShowed += OnShowed;
    }

    private void OnShowed()
    {
        _interstaialPanel.AdsShowed -= OnShowed;
        Completed?.Invoke();
    }
}