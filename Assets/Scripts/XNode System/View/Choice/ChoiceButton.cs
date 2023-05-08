using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _selfButton;

    public Node Node { get; private set; }
    public string ChoiceName => _text.text;
    public Button Button => _selfButton;

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
    }

    public void Initialized(ChoiseElement choiseElement)
    {
        Node = choiseElement.Node;
        _text.text = choiseElement.Text;
    }
}