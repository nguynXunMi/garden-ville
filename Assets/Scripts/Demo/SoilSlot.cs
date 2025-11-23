using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

namespace Demo
{
    public class SoilSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private Image soilImage;
        
        [Header("Timer")]
        [SerializeField] private float growDelay;
        [SerializeField] private GameObject timerGameObject;
        [SerializeField] private Image timerImage;
    
        private Sprite _plantSprite;
        private Sprite _sproutSprite;

        private bool _planted = false;
        private bool _isGrowing = false;
        private float _timerCount;
    
        private void Start()
        {
            soilImage.gameObject.SetActive(false);
            timerGameObject.SetActive(false);
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
            _plantSprite = seed.PlantSprite;
            _sproutSprite = seed.SproutSprite;
    
            // hide seed after planting
            seed.gameObject.SetActive(false);
    
            // change soil to sprout
            soilImage.sprite = _sproutSprite;
            soilImage.gameObject.SetActive(true);
    
            // start growth
            StartCoroutine(GrowPlant());
        }

        private void Update()
        {
            if (_isGrowing)
            {
                _timerCount += Time.deltaTime;
                timerImage.fillAmount = _timerCount / growDelay;
            }
        }

        private IEnumerator GrowPlant()
        {
            _timerCount = 0f;
            _isGrowing = true;
            timerGameObject.SetActive(true);
            yield return new WaitForSeconds(growDelay);
            _isGrowing = false;
            timerGameObject.SetActive(false);

            // final plant
            soilImage.sprite = _plantSprite;
            var ratio = _plantSprite.rect.height / _plantSprite.rect.width;
            var y = soilImage.rectTransform.sizeDelta.x * ratio;
            soilImage.rectTransform.sizeDelta = new Vector2(soilImage.rectTransform.sizeDelta.x, y) * 0.5f;
            soilImage.rectTransform.anchoredPosition = new Vector2(0, soilImage.rectTransform.sizeDelta.y * 0.5f + 20f);
        }
    }
}