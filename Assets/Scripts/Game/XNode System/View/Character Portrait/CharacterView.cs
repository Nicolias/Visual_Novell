using Characters;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class CharacterView : MonoBehaviour
{
    private Image _selfImage;
    private Button _selfButton;
    private CharacterType _characterType;

    private Quiz _quiz;

    public Image Image => _selfImage;

    public void Initialize(CharacterType character, Quiz quiz)
    {
        _characterType = character;
        _quiz = quiz;
    }

    private void Awake()
    {
        _selfImage = GetComponent<Image>();
        _selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveListener(OnClicked);
    }

    private void OnClicked()
    {
        _quiz.StartQuiz(_characterType);
    }
}