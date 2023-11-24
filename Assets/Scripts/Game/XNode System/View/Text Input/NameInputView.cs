using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInputView : MonoBehaviour, ITextInputView
{
    [SerializeField] private Canvas _selfCanvas;

    [SerializeField] private TMP_InputField _nameInputField;

    [SerializeField] private Button _completeNicknameButton;

    private TouchScreenKeyboard _keyboard;

    public event Action<string> TextInput;

    private void OnEnable()
    {
        _completeNicknameButton.onClick.AddListener(ComplteNickname);
    }

    private void OnDisable()
    {
        _completeNicknameButton.onClick.RemoveListener(ComplteNickname);
    }

    private void Update()
    {
        if(_keyboard != null)
            if(_keyboard.active)
                _nameInputField.text = _keyboard.text;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        enabled = true;
    }

    public void ShowKeyBoard()
    {
        _keyboard = TouchScreenKeyboard.Open("");
    }

    public void CloseKeyBoard()
    {
        _keyboard.active = false;
        _keyboard = null;
    }

    private void ComplteNickname()
    {
        string nickname = _nameInputField.text.Clone().ToString();
        nickname = nickname.Replace(" ", "");

        if (nickname == "")
            return;

        TextInput?.Invoke(_nameInputField.text);

        gameObject.SetActive(false);
        enabled = false;
    }
}