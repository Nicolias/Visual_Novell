﻿using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemForCollectionView : MonoBehaviour
{
    [SerializeField] private Image _itemImage;

    private ItemForCollection _itemData;
    private Button _selfButton;

    public event Action<ItemForCollectionView, ItemForCollection> ItemSelected;

    private void Awake()
    {
        _selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(() =>
        {
            if(_itemData.IsInteractable)
                ItemSelected?.Invoke(this, _itemData);
        });
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
    }

    public void Initialize(ItemForCollection itemData)
    {
        _itemData = itemData;

        _itemImage.sprite = itemData.ItemSpriteInScene;
    }
}
