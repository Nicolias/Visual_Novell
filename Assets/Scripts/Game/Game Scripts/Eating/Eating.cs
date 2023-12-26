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
    private TimesOfDayServise _timesOfDayServise;
    private ICharacter _currentCharacter;

    public event Action Closed;

    [Inject]
    public void Construct(IChoicePanelFactory choicePanelFactory, Wallet wallet, TimesOfDayServise timesOfDayServise)
    {
        _choicePanel = choicePanelFactory.CreateChoicePanel(transform);
        _wallet = wallet;
        _timesOfDayServise = timesOfDayServise;
    }

    public void Enter(ICharacter character)
    {
        DateTime currentTime = _timesOfDayServise.CurrentTime;

        _menu.Open();  
        _currentCharacter = character;
        _selfCanvas.enabled = true;
        _menu.Closed += Exit;

        OnSympathyPointsChanged(character.SympathyPoints);
        OnMoneyCountChanged(_wallet.CurrentValue);

        if (_timesOfDayServise.GetCurrentTimesOfDay() == character.LastEatingTimeOfDay)
        {
            if (currentTime.Year <= character.LastEatingTime.Year &&
                currentTime.Month <= character.LastEatingTime.Month &&
                currentTime.Day <= character.LastEatingTime.Day)
            {

                _menu.ProductSelected += CharacterFeeded;
                return;
            }
        }

        character.SympathyPointsChanged += OnSympathyPointsChanged;
        _wallet.ValueChanged += OnMoneyCountChanged;
        _menu.ProductSelected += OnProductSelected;
    }

    private void Exit()
    {
        _currentCharacter.SympathyPointsChanged -= OnSympathyPointsChanged;
        _wallet.ValueChanged -= OnMoneyCountChanged;

        _selfCanvas.enabled = false;

        _menu.ProductSelected -= OnProductSelected;
        _menu.ProductSelected -= CharacterFeeded;
        _menu.Closed -= Exit;

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
        if (_wallet.CurrentValue >= product.Price)
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

        _menu.Close();
    }

    private void CharacterFeeded(EatingProduct product)
    {
        _choicePanel.Show("Персонаж уже ел в это время суток", new List<ChoiseElement>() { new ChoiseElement("Принять", () =>
        { 
            Exit(); 
            _choicePanel.Hide();
        }) });
    }
}
