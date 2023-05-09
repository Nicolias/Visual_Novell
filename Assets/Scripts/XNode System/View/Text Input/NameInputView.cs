using System;
using UnityEngine;
using TMPro;
using XNode;
using UnityEngine.UI;

public class NameInputView : MonoBehaviour, ITextInputView
{
    public event Action<string> OnTextInput;

    [SerializeField] private Canvas _selfCanvas;

    [SerializeField] private TMP_InputField _nameInputField;

    [SerializeField] private Button _completeNicknameButton;

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

        OnTextInput?.Invoke(_nameInputField.text);

        HideCanvas();
    }    

    private void ShowCanvas() => _selfCanvas.gameObject.SetActive(true);

    private void HideCanvas()
    {
        _selfCanvas.gameObject.SetActive(false);
    }
}