using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DUXWindow : MonoBehaviour, IDUXVisitor
{
    public event Action OnClosed;

    [SerializeField] private Canvas _selfCanvas;
    [SerializeField] private Button _closeButton;

    [SerializeField] private Image _mainImage;
    [SerializeField] private TMP_Text _characterSympathyPoints;
    [SerializeField] private TMP_Text _discriptionText;
    [SerializeField] private Button _backButton;

    [SerializeField] private List<DUXCategoryData> _categories;
    [SerializeField] private Transform _categoriesContainer;
    [SerializeField] private ChoiceButton _categoryButtonTemplate;

    [SerializeField] private AudioPlayer _audioPlayer;

    private CharactersLibrary _charactersLibrary;

    private Stack<List<DUXCategoryData>> _categoriesStack = new();

    [Inject]
    public void Construct(CharactersLibrary charactersLibrary)
    {
        _charactersLibrary = charactersLibrary;
    }

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
        _characterSympathyPoints.text = "";

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
                _characterSympathyPoints.text = "";
            }

            _audioPlayer.Hide();
        });

        foreach (Transform category in _categoriesContainer)
            Destroy(category.gameObject);

        foreach (var category in categories)
        {
            var newCategoryButton = Instantiate(_categoryButtonTemplate, _categoriesContainer);
            newCategoryButton.Button.interactable = category.IsBlocked(_charactersLibrary) == false;

            newCategoryButton.Initialized(new(category.CategoryName, () =>
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

        _characterSympathyPoints.text = $"Симпатия: {_charactersLibrary.GetCharacter(characterCategoryData.CharacterType).SympathyPoints}";
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

    public void Visit(MelodyCategoryData melodyCategoryData)
    {
        _discriptionText.text = melodyCategoryData.Discription;
        _audioPlayer.SetAudioClip(melodyCategoryData.AudioClip);
        _audioPlayer.Show();
    }

    public void Visit(DiscriptionWithImageData discriptionWithImageData)
    {
        _mainImage.sprite = discriptionWithImageData.Sprite;
        _mainImage.color = new(1, 1, 1, 1);
        _discriptionText.text = discriptionWithImageData.Discription;
    }
}

#region class
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