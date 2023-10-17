﻿using System;
using Zenject;

public class CollectionQuestController : IController
{
    public event Action OnComplete;

    private readonly CollectQuestModel _data;
    private readonly CollectionQuestView _view;
    private readonly DiContainer _di;
    
    private CollectionQuest _model;

    public CollectionQuestController(CollectQuestModel model, DiContainer di, CollectionQuestView view)
    {
        _data = model;
        _di = di;
        _view = view;
    }

    public void Execute()
    {
        _model = _di.Instantiate<CollectionQuest>(new object[] { _data.ItemForCollections });
        _view.Initialize(_model);

        _model.QuestCompleted += CallBack;
    }

    private void CallBack()
    {
        _model.QuestCompleted -= CallBack;

        _view.Dispose();
        _model.Dispose();

        OnComplete?.Invoke();
    }
}
