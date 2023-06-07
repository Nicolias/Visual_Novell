using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DUXWindow : MonoBehaviour, IDUXVisitor
{
    public event Action OnClosed;

    [SerializeField] private Canvas _selfCanvas;
    [SerializeField] private Button _closeButton;

    [SerializeField] private Image _mainImage;
    [SerializeField] private TMP_Text _discriptionText;

    [SerializeField] private List<DUXCategoryData> _categories;
    [SerializeField] private Transform _categoriesContainer;
    [SerializeField] private ChoiceButton _categoryButtonTemplate;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(Hide);
        ShowCategory(_categories);
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
        _mainImage.color = new(1, 1, 1, 0);
    }

    private void ShowCategory(List<DUXCategoryData> categories)
    {
        foreach (Transform category in _categoriesContainer)
            Destroy(category.gameObject);

        foreach (var category in categories)
        {
            var newCategory = Instantiate(_categoryButtonTemplate, _categoriesContainer);

            newCategory.Initialized(new(category.CategoryName, () =>
            {
                if(category.Subcategories.Count > 0)
                    ShowCategory(category.Subcategories);

                category.Accept(this);
            }));
        }
    }

    public void Visit(CharacterCategoryData characterCategoryData)
    {
        _mainImage.sprite = characterCategoryData.CharacterSprite;
        _mainImage.color = new(1, 1, 1, 1);
        _discriptionText.text = characterCategoryData.Discription;
    }
}







#region class
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
#endregion