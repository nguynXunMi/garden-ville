using System;
using Main.Controllers;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace Main
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text counterText;
        
        [Header("Sell")]
        [SerializeField] private Button sellButton;
        [SerializeField] private TMP_Text sellButtonText;
        
        private Vector2 _baseSizeDelta;
        private int _sellValue;

        public string SlotName { get; private set; }

        private void Awake()
        {
            _baseSizeDelta = image.rectTransform.sizeDelta;
            sellButton.onClick.AddListener(OnClickSellButton);
        }

        private void OnDestroy()
        {
            sellButton.onClick.RemoveListener(OnClickSellButton);
        }

        private void OnClickSellButton()
        {
            AudioController.Instance?.PlaySellSfx();
            Inventory.Instance.SellItem(SlotName, _sellValue);
            Destroy(gameObject);
        }

        public void SetData(string slotName, int sellValue, Sprite sprite)
        {
            SlotName = slotName;
            _sellValue = sellValue;
            sellButtonText.text = $"{_sellValue}";
            image.sprite = sprite;
        }

        public void SetImage(Sprite sprite)
        {
            // image.SetNativeSize();
            // image.rectTransform.sizeDelta *= 2;
            // image.rectTransform.anchoredPosition += new Vector2(0f, 0f);
            // AdjustImage(sprite, image);
        }

        public void SetText(string text)
        {
            counterText.text = text;
        }
        
        private void AdjustImage(Sprite sprite, Image img)
        {
            var ratio = sprite.rect.height / sprite.rect.width;
            var y = _baseSizeDelta.x * ratio;
            img.rectTransform.sizeDelta = new Vector2(_baseSizeDelta.x, y);
            img.rectTransform.anchoredPosition = new Vector2(0, img.rectTransform.sizeDelta.y * 0.5f);
        }
    }
}