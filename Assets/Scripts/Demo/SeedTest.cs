using UnityEngine.Tilemaps;

namespace Demo
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class SeedTest : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;
        private Transform originalParent;
        private Vector2 originalAnchoredPos;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            originalParent = transform.parent;
            originalAnchoredPos = rectTransform.anchoredPosition;
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
            Vector3 worldPos = ScreenToTilemapWorld(eventData.position, SoilTilemap.Instance.soilTilemap);

            // Try to plant using the SoilTilemap singleton
            bool planted = SoilTilemap.Instance != null && SoilTilemap.Instance.TryPlantAt(worldPos);

            if (planted)
            {
                // hide or destroy the seed icon (consumed)
                gameObject.SetActive(false);
            }
            else
            {
                // return to inventory slot
                transform.SetParent(originalParent);
                rectTransform.anchoredPosition = originalAnchoredPos;
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
        float GetCanvasScaleFactor()
        {
            var canvas = GetComponentInParent<Canvas>();
            return canvas != null ? canvas.scaleFactor : 1f;
        }
    }
}