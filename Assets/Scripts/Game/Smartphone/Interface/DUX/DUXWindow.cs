﻿using System;
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
    [SerializeField] private Button _backButton;

    [SerializeField] private List<DUXCategoryData> _categories;
    [SerializeField] private Transform _categoriesContainer;
    [SerializeField] private ChoiceButton _categoryButtonTemplate;

    private Stack<List<DUXCategoryData>> _categoriesStack = new();

    public void Open()
    {
        _selfCanvas.enabled = true;
        _closeButton.onClick.AddListener(Hide);
        ShowCategory(_categories);
    }

    private void Hide()
    {
        _selfCanvas.enabled = false;
        _mainImage.color = new(1, 1, 1, 0);
        _discriptionText.text = "";

        OnClosed?.Invoke();
        _closeButton.onClick.RemoveAllListeners();
    }

    private void ShowCategory(List<DUXCategoryData> categories)
    {
        _backButton.onClick.RemoveAllListeners();
        _backButton.onClick.AddListener(() =>
        {
            if (_categoriesStack.Count > 0)
            {
                ShowCategory(_categoriesStack.Pop());
                _mainImage.color = new(1, 1, 1, 0);
                _discriptionText.text = "";
            }
        });

        foreach (Transform category in _categoriesContainer)
            Destroy(category.gameObject);

        foreach (var category in categories)
        {
            var newCategory = Instantiate(_categoryButtonTemplate, _categoriesContainer);

            newCategory.Initialized(new(category.CategoryName, () =>
            {
                if (category.Subcategories.Count > 0)
                {
                    ShowCategory(category.Subcategories);
                    _categoriesStack.Push(categories);
                }

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

    public void Visit(CharacterImageVariationCategoryData characterImageVariationCategoryData)
    {
        _mainImage.sprite = characterImageVariationCategoryData.CharacterSprite;
        _mainImage.color = new(1, 1, 1, 1);
    }

    public void Visit(CharacterImageCategoryData characterImageCategoryData)
    {
        _discriptionText.text = characterImageCategoryData.Discription;
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