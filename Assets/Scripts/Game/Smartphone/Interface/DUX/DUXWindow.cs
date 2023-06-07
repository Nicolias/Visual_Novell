using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DUXWindow : MonoBehaviour
{
    public event Action OnClosed;

    [SerializeField] private Canvas _selfCanvas;
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
        OnClosed?.Invoke();
    }

    public void Open()
    {
        _selfCanvas.enabled = true;
    }

    private void Hide()
    {
        _selfCanvas.enabled = false;
    }
}

public class DUXCategoryData : ScriptableObject
{
    [field: SerializeField] public string CategoryName { get; private set; }
    [field: SerializeField] public List<DUXCategoryData> Subcategories { get; private set; }
}

public class CharacterCategoryData : DUXCategoryData
{

}

public class AboutCharacterCategoryData : DUXCategoryData
{

}

public class CharacterStoryCategoryData : DUXCategoryData
{

}

public class CharacterSpeechCategoryData : DUXCategoryData
{

}

public class CharacterImageCategoryData : DUXCategoryData
{

}

public class LocationCategoryData : DUXCategoryData
{

}

public class MelodyCategoryData : DUXCategoryData
{

}

public class QuoteCategoryData : DUXCategoryData
{

}

public class ConstellationCategoryData : DUXCategoryData
{

}

public class StarsCategoryData : DUXCategoryData
{

}

public class TutorialCategoryData : DUXCategoryData
{

}