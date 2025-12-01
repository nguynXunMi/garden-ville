using Main.Plant;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Obsolete
{
    public class SeedDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image image;
        
        private Transform _originalParent;
        private Vector2 _originalAnchoredPos;

        public Sprite PlantSprite { get; private set; }
        public Sprite SproutSprite { get; private set; }
        public Sprite CollectibleSprite { get; private set; }

        public void SetData(Plant data)
        {
            var seedSprite = data.SeedSprite;
            PlantSprite = data.PlantSprite;
            SproutSprite = data.SproutSprite;
            CollectibleSprite = data.CollectibleSprite;
            image.sprite = seedSprite;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalParent = transform.parent;
            _originalAnchoredPos = rectTransform.anchoredPosition;
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
            rectTransform.anchoredPosition = _originalAnchoredPos;
        }
    }
}