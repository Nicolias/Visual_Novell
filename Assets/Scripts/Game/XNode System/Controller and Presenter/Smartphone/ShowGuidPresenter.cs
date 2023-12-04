using System;

public class ShowGuidPresenter : IPresentar
{
    public event Action Complete;

    private ShowGuidModel _guidModel;
    private GuidView _guidView;

    public ShowGuidPresenter(ShowGuidModel guidModel, GuidView guidView)
    {
        _guidModel = guidModel;
        _guidView = guidView;
    }

    public void Execute()
    {
        _guidView.Show(_guidModel.GuidSprite);
        _guidView.Closed += CallBack;
    }

    private void CallBack()
    {
        _guidView.Closed -= CallBack;
        Complete?.Invoke();
    }
}