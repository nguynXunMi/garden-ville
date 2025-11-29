using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text counterText;
        
        public string SlotName => image.sprite?.name ?? string.Empty;
        private Vector2 _baseSizeDelta;

        private void Awake()
        {
            _baseSizeDelta = image.rectTransform.sizeDelta;
        }

        public void SetImage(Sprite sprite)
        {
            image.sprite = sprite;
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