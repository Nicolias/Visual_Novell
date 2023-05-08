using System;
using UnityEngine;
using TMPro;
using XNode;
using UnityEngine.UI;

public class NameInputView : MonoBehaviour, ITextInputView
{
    public event Action<Node> OnChoiceMade;
    public event Action<string> OnTextInput;

    [SerializeField] private Canvas _selfCanvas;

    [SerializeField] private TMP_InputField _nameInputField;

    [SerializeField] private Button _completeNicknameButton, _unCompleteNicknameButton;
    [SerializeField] private Node _completeNode, _unCompleteNode;

    public Canvas Canvas => _selfCanvas;

    private void OnDisable()
    {
        _completeNicknameButton.onClick.RemoveAllListeners();
        _unCompleteNicknameButton.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        ShowCanvas();

        _completeNicknameButton.onClick.AddListener(ComplteNickname);
        _unCompleteNicknameButton.onClick.AddListener(UnComplteNickname);        
    }

    private void ComplteNickname()
    {
        string nickname = _nameInputField.text.Clone().ToString();
        nickname = nickname.Replace(" ", "");

        if (nickname == "")
            return;

        OnTextInput?.Invoke(_nameInputField.text);
        OnChoiceMade?.Invoke(_completeNode);

        HideCanvas();
    }

    private void UnComplteNickname()
    {
        OnTextInput?.Invoke("Везунчик");
        OnChoiceMade?.Invoke(_unCompleteNode);

        HideCanvas();
    }

    private void ShowCanvas() => _selfCanvas.gameObject.SetActive(true);

    private void HideCanvas()
    {
        _selfCanvas.gameObject.SetActive(false);
    }
}