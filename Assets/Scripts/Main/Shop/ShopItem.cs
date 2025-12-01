using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Shop
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private CanvasGroup lockCanvasGroup;
        [SerializeField] private Button shopItemButton;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private Image itemImage;
        
        private bool _isPurchased;
        private int _itemId;

        public void SetData()
        {
            
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
            lockCanvasGroup.alpha = 1f;
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
            
        }
    }
}