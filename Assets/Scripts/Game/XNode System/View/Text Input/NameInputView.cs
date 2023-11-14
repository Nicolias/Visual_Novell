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

    private void OnDisable()
    {
        _completeNicknameButton.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        ShowCanvas();

        _completeNicknameButton.onClick.AddListener(ComplteNickname);     
    }

    private void ComplteNickname()
    {
        string nickname = _nameInputField.text.Clone().ToString();
        nickname = nickname.Replace(" ", "");

        if (nickname == "")
            return;

        TextInput?.Invoke(_nameInputField.text);

        HideCanvas();
    }

    public void ShowKeyBoard()
    {
        _keyboard = TouchScreenKeyboard.Open("");
    }

    public void CloseKeyBoard()
    {
        _nameInputField.text = _keyboard.text;
        _keyboard.active = false;
    }

    private void ShowCanvas() => _selfCanvas.enabled = true;

    private void HideCanvas()
    {
        _selfCanvas.enabled = false;
    }
}