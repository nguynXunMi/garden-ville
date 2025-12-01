using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ShopData shopData;
        [SerializeField] private ShopItem shopItemPrefab;
        [SerializeField] private RectTransform itemParent;

        public static Shop Instance;
        
        private List<ShopItem> ShopItems { get; }= new();
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            UpdateData();
        }

        private void UpdateData()
        {
            if (shopData == null)
            {
                return;
            }

            foreach (var item in shopData.ShopItemDataList)
            {
                var shopItem = Instantiate(shopItemPrefab, itemParent);
                shopItem.SetData(item);
                shopItem.UpdateStatus(Inventory.Instance.PlayerGold);
                ShopItems.Add(shopItem);
            }
        }

        public void PurchaseItem(int itemId, int price)
        {
            var shopItem = ShopItems.Find(item => item.ItemId == itemId);
            if (shopItem == null)
            {
                return;
            }

            Inventory.Instance.OnPurchased(price);
            OnUpdateShopItemStatus();
        }

        public void OnOpenShop()
        {
            OnUpdateShopItemStatus();
        }

        private void OnUpdateShopItemStatus()
        {
            if (ShopItems == null || ShopItems.Count == 0)
            {
                return;
            }

            foreach (var shopItem in ShopItems)
            {
                shopItem.UpdateStatus(Inventory.Instance.PlayerGold);
            }
        }
    }
}