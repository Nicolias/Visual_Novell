using System;
using Zenject;

public class CollectionQuestController : IController
{
    public event Action OnComplete;

    private readonly CollectQuestModel _model;
    private readonly CollectionQuestView _view;
    private readonly DiContainer _di;
    
    private CollectionQuest _collectionQuest;

    public CollectionQuestController(CollectQuestModel model, DiContainer di, CollectionQuestView view)
    {
        _model = model;
        _di = di;
        _view = view;
    }

    public void Execute()
    {
        _collectionQuest = _di.Instantiate<CollectionQuest>(new object[] { _model.ItemForCollections });
        _view.Initialize(_collectionQuest);
        _collectionQuest.OnQuestCompleted += CallBack;
    }

    private void CallBack()
    {
        _collectionQuest.OnQuestCompleted -= CallBack;

        OnComplete?.Invoke();
    }
}
