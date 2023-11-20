using System;
using Zenject;

public class CollectionQuestController : IController
{
    public event Action Complete;

    private readonly CollectQuestModel _data;
    private readonly CollectionQuestView _view;
    private readonly DiContainer _di;
    
    private CollectionItmsQuest _model;

    public CollectionQuestController(CollectQuestModel model, DiContainer di, CollectionQuestView view)
    {
        _data = model;
        _di = di;
        _view = view;
    }

    public void Execute()
    {
        _model = _di.Instantiate<CollectionItmsQuest>(new object[] { _data.ItemForCollections });
        _view.Initialize(_model);

        _model.QuestCompleted += CallBack;
    }

    private void CallBack()
    {
        _model.QuestCompleted -= CallBack;

        Complete?.Invoke();
    }
}
