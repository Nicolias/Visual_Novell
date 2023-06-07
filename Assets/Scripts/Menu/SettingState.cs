public class SettingState : BaseState
{
    public SettingWindow _settingWindow;

    public SettingState(SettingWindow settingWindow)
    {
        _settingWindow = settingWindow;
    }

    public override void Entry()
    {
        _settingWindow.Show();
    }

    public override void Exit()
    {
        _settingWindow.Hide();
    }
}