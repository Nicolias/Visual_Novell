using Characters;
using EatingSystem;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class Eating : MonoBehaviour, ICloseable
{
    [SerializeField] private Canvas _selfCanvas;

    [SerializeField] private TMP_Text _sympathyPointsText;
    [SerializeField] private TMP_Text _moneyInWalletText;

    [SerializeField] private MenuPanel _menu;

    private ChoicePanel _choicePanel;
    private Wallet _wallet;

    private Character _currentCharacter;

    public event Action Closed;

    [Inject]
    public void Construct(IChoicePanelFactory choicePanelFactory, Wallet wallet)
    {
        _choicePanel = choicePanelFactory.CreateChoicePanel(transform);
        _wallet = wallet;
    }

    private void OnEnable()
    {
        _menu.ProductSelected += OnProductSelected;
        _menu.Closed += Exit;
    }

    private void OnDisable()
    {
        _menu.ProductSelected -= OnProductSelected;
        _menu.Closed -= Exit;
    }

    public void Enter(Character character)
    {
        _menu.Open();

        _currentCharacter = character;

        character.SympathyPointsChanged += OnSympathyPointsChanged;
        _wallet.MoneyChanged += OnMoneyCountChanged;

        _selfCanvas.enabled = true;

        OnSympathyPointsChanged(character.SympathyPoints);
        OnMoneyCountChanged(_wallet.MoneyCount);
    }

    private void Exit()
    {
        _currentCharacter.SympathyPointsChanged -= OnSympathyPointsChanged;
        _wallet.MoneyChanged -= OnMoneyCountChanged;

        _selfCanvas.enabled = false;

        Closed?.Invoke();
    }

    private void OnSympathyPointsChanged(int sympathyPoints)
    {
        _sympathyPointsText.text = "Симпатия: " + sympathyPoints.ToString(); 
    }

    private void OnMoneyCountChanged(int moneyCount)
    {
        _moneyInWalletText.text = "Осталось денег: " + moneyCount.ToString();
    }

    private void OnProductSelected(EatingProduct product)
    {
        _choicePanel.Show($"Вы уверены что хотите купить {product.Name}", new List<ChoiseElement>() 
        { 
            new ChoiseElement("Да", () => TryBuyProduct(product)),
            new ChoiseElement("Нет", () => _choicePanel.Hide())
        });
    }

    private void TryBuyProduct(EatingProduct product)
    {
        if (_wallet.MoneyCount >= product.Price)
        {
            _wallet.Decreese(product.Price);

            _currentCharacter.Feed(product);
            _choicePanel.Hide();
        }
        else
        {
            _choicePanel.Show("Недостаточно средств.", new List<ChoiseElement>()
            {
                new ChoiseElement("Закрыть", () => _choicePanel.Hide())
            });
        }
    }
}
