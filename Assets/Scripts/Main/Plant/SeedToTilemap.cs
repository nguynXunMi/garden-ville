using Main.Soil;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine;

namespace Main.Plant
{
    public class SeedToTilemap : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image image;

        private Transform _originalParent;
        private Vector2 _originalAnchoredPos;
        private Plant Data { get; set; }

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }
        
        public void SetData(Plant data)
        {
            var seedSprite = data.SeedSprite;
            Data = data;
            image.sprite = seedSprite;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalParent = transform.parent;
            _originalAnchoredPos = rectTransform.anchoredPosition;
            transform.SetAsLastSibling();
            canvasGroup.blocksRaycasts = false; // allow world checks if UI raycasts used
        }

        public void OnDrag(PointerEventData eventData)
        {
            // move using screen delta (works for overlays). If using Screen Space - Camera you may need
            // to convert differently, but this is fine for most Canvas setups.
            rectTransform.anchoredPosition += eventData.delta / GetCanvasScaleFactor();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;

            // Convert pointer position -> world position
            Vector3 worldPos = ScreenToTilemapWorld(eventData.position, SoilTilemap.Instance.Tilemap);

            // Try to plant using the SoilTilemap singleton
            bool planted = SoilTilemap.Instance != null && SoilTilemap.Instance.TryPlantAt(worldPos, Data);
            if (planted)
            {
                // hide or destroy the seed icon (consumed)
                // gameObject.SetActive(false);
                transform.SetParent(_originalParent);
                rectTransform.anchoredPosition = _originalAnchoredPos;
            }
            else
            {
                // return to inventory slot
                transform.SetParent(_originalParent);
                rectTransform.anchoredPosition = _originalAnchoredPos;
            }
        }
        
        public static Vector3 ScreenToTilemapWorld(Vector2 screenPos, Tilemap tilemap)
        {
            float zDistance = Mathf.Abs(Camera.main.transform.position.z - tilemap.transform.position.z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(screenPos.x, screenPos.y, zDistance)
            );

            worldPos.z = tilemap.transform.position.z;
            return worldPos;
        }


        // helper for canvases with scaleFactor
        private float GetCanvasScaleFactor()
        {
            var canvas = GetComponentInParent<Canvas>();
            return canvas != null ? canvas.scaleFactor : 1f;
        }
    }
}