using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class ChoiseButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _selfButton;


    public Dialog Dialog { get; set; }
    public Node Node { get; private set; }

    public void Set()
    {
        _selfButton.onClick.AddListener(() => Dialog.Choise(this));
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
    }

    public void Show(ChoiseElement choiseElement)
    {
        _text.text = choiseElement.Text;
        Node = choiseElement.Node;
        _selfButton.onClick.AddListener(() => Dialog.Choise(this));
    }

    public void Hide() => Destroy(gameObject);
}