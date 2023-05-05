using System;
using UnityEngine;

public class Choise : MonoBehaviour
{
    public event Action OnEnded;

    [SerializeField] private Canvas _selfCanvas;
    [SerializeField] private Transform _parrent;
    [SerializeField] private ChoiseButton _choiseButtonTemplate;

    public Canvas Canvas => _selfCanvas;

    private void Start()
    {
        _selfCanvas.enabled = false;   
    }

    public void Show() => _selfCanvas.enabled = true;

    public void Hide() => _selfCanvas.enabled = false;

    public void Add(ChoiseElement choiseElement, Dialog dialog)
    {
        Show();
       
        var choise = Instantiate(_choiseButtonTemplate, _parrent);
        choise.Dialog = dialog;
        choise.Show(choiseElement);
        
    }

    public void OnClick(ChoiseButton choiseButton)
    {
        OnEnded?.Invoke();
        Hide();
    }
}
