using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Main.Shop
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private CanvasGroup lockCanvasGroup;
        [SerializeField] private Button shopItemButton;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private Image itemImage;
        
        public bool IsPurchased { get; private set; }
        public int ItemId { get; private set; }
        private int _price;
        private Plant.Plant _plantData;

        public void SetData(ShopItemData data)
        {
            if (data == null)
            {
                return;
            }

            ItemId = data.Id;
            _price = data.Price;
            _plantData = data.Plant;
            itemImage.sprite = data.Sprite;
            priceText.text = $"{_price}";
        }

        public void UpdateStatus(int gold)
        {
            if (IsPurchased)
            {
                SetItemPurchased();
                return;
            }
            
            if (_price > gold)
            {
                SetItemUnpurchasable();
            }
            else
            {
                SetItemPurchasable();
            }
        }

        private void OnPurchased()
        {
            IsPurchased = true;
            SetItemPurchased();
        }
        
        private void SetItemUnpurchasable()
        {
            lockCanvasGroup.alpha = 0.5f;
            lockCanvasGroup.interactable = true;
            lockCanvasGroup.blocksRaycasts = true;
        }

        private void SetItemPurchasable()
        {
            lockCanvasGroup.alpha = 0f;
            lockCanvasGroup.interactable = false;
            lockCanvasGroup.blocksRaycasts = false;
        }

        private void SetItemPurchased()
        {
            lockCanvasGroup.alpha = 0.75f;
            lockCanvasGroup.interactable = true;
            lockCanvasGroup.blocksRaycasts = true;
        }

        private void Awake()
        {
            shopItemButton.onClick.AddListener(OnClickShopItem);
        }

        private void OnDestroy()
        {
            shopItemButton.onClick.RemoveListener(OnClickShopItem);
        }

        private void OnClickShopItem()
        {
            Shop.Instance.PurchaseItem(ItemId, _price);
            
            // TODO: Add proper callback event to invoke when purchase is made successful
            OnPurchased();
        }
    }
}