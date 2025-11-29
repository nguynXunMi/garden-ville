using Main.Plant;
using System.Collections;
using Obsolete;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

namespace Main.Soil
{
    public class SoilSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private Button button;
        [SerializeField] private Image soilImage;
        [SerializeField] private Sprite[] soilSprites;
        
        [Header("Timer")]
        [SerializeField] private float growDelay;
        [SerializeField] private GameObject timerGameObject;
        [SerializeField] private Image timerImage;
    
        private Sprite _plantSprite;
        private Sprite _sproutSprite;
        private Sprite _collectibleSprite;

        private bool _planted = false;
        private bool _isGrowing = false;
        private bool _isCollectable = false;
        private float _timerCount;
        private Vector2 _baseSizeDelta;

        private void Awake()
        {
            soilImage.sprite = soilSprites[new System.Random().Next(soilSprites.Length)];
            _baseSizeDelta = soilImage.rectTransform.sizeDelta;
        }

        private void Start()
        {
            soilImage.gameObject.SetActive(false);
            timerGameObject.SetActive(false);
            button.interactable = false;
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
            _collectibleSprite = seed.CollectibleSprite;
    
            // hide seed after planting
            // seed.gameObject.SetActive(false);
    
            // change soil to sprout
            SetSoilAlpha(1f);
            soilImage.sprite = _sproutSprite;
            soilImage.gameObject.SetActive(true);
            AdjustImage(_sproutSprite, soilImage);
    
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
            _isCollectable = true;
            button.interactable = true;

            soilImage.sprite = _plantSprite;
            AdjustImage(_plantSprite, soilImage);
        }

        public void OnClickToCollect()
        {
            Inventory.Instance.AddCollectedItem(_collectibleSprite);
            soilImage.sprite = null;
            SetSoilAlpha(0f);
            button.interactable = false;
            _planted = false;
            _isCollectable = false;
        }

        private void SetSoilAlpha(float alpha)
        {
            var color = soilImage.color;
            color.a = alpha;
            soilImage.color = color;
        }

        private void AdjustImage(Sprite sprite, Image image)
        {
            var ratio = sprite.rect.height / sprite.rect.width;
            var y = _baseSizeDelta.x * ratio;
            image.rectTransform.sizeDelta = new Vector2(_baseSizeDelta.x, y);
            image.rectTransform.anchoredPosition = new Vector2(0, image.rectTransform.sizeDelta.y * 0.5f + 20f);
        }
    }
}