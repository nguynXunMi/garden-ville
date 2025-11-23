using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Demo
{
    public class SoilSlot : MonoBehaviour, IDropHandler
    {
        public Image soilImage;
        public Sprite sproutSprite;
        public Sprite plantSprite;
    
        public float growDelay = 3f;
    
        private bool _planted = false;
    
        private void Start()
        {
            if (soilImage == null)
            {
                soilImage = GetComponent<Image>();
            }
            
            soilImage.gameObject.SetActive(false);
        }
    
        public void OnDrop(PointerEventData eventData)
        {
            if (_planted)
            {
                return;
            }
    
            var seed = eventData.pointerDrag?.GetComponent<SeedDraggable>();
            if (seed == null)
            {
                return;
            }

            _planted = true;
            plantSprite = seed.PlantSprite;
    
            // hide seed after planting
            seed.gameObject.SetActive(false);
    
            // change soil to sprout
            soilImage.sprite = sproutSprite;
            soilImage.gameObject.SetActive(true);
    
            // start growth
            StartCoroutine(GrowPlant());
        }
    
        IEnumerator GrowPlant()
        {
            yield return new WaitForSeconds(growDelay);
    
            // final plant
            soilImage.sprite = plantSprite;
            var ratio = plantSprite.rect.height / plantSprite.rect.width;
            var y = soilImage.rectTransform.sizeDelta.x * ratio;
            soilImage.rectTransform.sizeDelta = new Vector2(soilImage.rectTransform.sizeDelta.x, y) * 0.5f;
            soilImage.rectTransform.anchoredPosition = new Vector2(0, soilImage.rectTransform.sizeDelta.y * 0.5f + 20f);
        }
    }

}