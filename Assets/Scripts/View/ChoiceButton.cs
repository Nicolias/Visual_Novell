using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _selfButton;


    public Dialog Dialog { get; private set; }
    public Node Node { get; private set; }
    public Button Button => _selfButton;

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
    }

    public void Initialized(Dialog dialog, ChoiseElement choiseElement)
    {
        Dialog = dialog;
        Node = choiseElement.Node;
        _text.text = choiseElement.Text;
    }
}