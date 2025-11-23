using Garden;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Demo
{
    public class SeedDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image image;
        
        private Transform _originalParent;
        public Sprite PlantSprite { get; private set; }

        public void SetData(Plant data)
        {
            var seedSprite = data.SeedSprite;
            PlantSprite = data.PlantSprite;
            image.sprite = seedSprite;
            // image.SetNativeSize();
            
            var ratio = seedSprite.rect.height / seedSprite.rect.width;
            var y = image.rectTransform.sizeDelta.x * ratio;
            image.rectTransform.sizeDelta = new Vector2(image.rectTransform.sizeDelta.x, y) * 0.5f;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalParent = transform.parent;
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
            transform.SetParent(_originalParent);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}