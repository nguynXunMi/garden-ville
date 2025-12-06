using System.Collections;
using System;
using Main.Controllers;
using UnityEngine.UI;
using UnityEngine;

namespace Main.Plant
{
    public class PlantObject : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Image progressBarImage;
        [SerializeField] private GameObject progressBar;

        [Header("Model")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform modelTransform;
        
        private Plant Data { get; set; }
        private Vector3Int Position { get; set; }

        private bool _isGrowing = false;
        private bool _isHarvestable = false;
        private float _timerCount;
        private float _sproutDuration;
        private float _growScale;

        public event Action<Vector3Int> Harvested;

        private void Update()
        {
            if (_isGrowing)
            {
                _timerCount += Time.deltaTime;
                progressBarImage.fillAmount = _timerCount / _sproutDuration;
            }
        }

        public void SetData(Plant data, int index, Vector3Int position, Camera mainCamera)
        {
            canvas.worldCamera = mainCamera;
            Data = data;
            Position = position;
            _sproutDuration = data.SproutDuration;
            spriteRenderer.sprite = data.SproutSprite;
            _growScale = data.Type == PlantType.Fruit ? 3.5f : 2.5f;
            progressBar.gameObject.SetActive(true);
            _timerCount = 0f;
            _isGrowing = true;
            spriteRenderer.sortingOrder -= index;
            // StartCoroutine(GrowCoroutine());
            StartCoroutine(GrowCoroutine(data.PlantSprites));
        }

        private IEnumerator GrowCoroutine()
        {
            yield return new WaitForSeconds(_sproutDuration);
            _isGrowing = false;
            progressBar.gameObject.SetActive(false);
            Grow();
        }

        private void Grow()
        {
            if (spriteRenderer == null)
            {
                return;
            }

            modelTransform.localScale = Vector3.one * _growScale;
            spriteRenderer.sprite = Data.PlantSprite;
            _isHarvestable = true;
        }

        private IEnumerator GrowCoroutine(Sprite[] sprites)
        {
            if (sprites is not { Length: > 0 })
            {
                StartCoroutine(GrowCoroutine());
                yield break;
            }

            var duration = _sproutDuration / sprites.Length;
            for (var i = 0; i < sprites.Length; i++)
            {
                yield return new WaitForSeconds(duration);
                var isHarvestable = i == sprites.Length - 1;
                if (isHarvestable)
                {
                    _isGrowing = false;
                    progressBar.gameObject.SetActive(false);
                }
                Grow(sprites[i], isHarvestable);
            }
        }

        private void Grow(Sprite plantSprite, bool isHarvestable)
        {
            if (spriteRenderer == null || plantSprite == null)
            {
                return;
            }

            modelTransform.localScale = Vector3.one * _growScale;
            spriteRenderer.sprite = plantSprite;
            _isHarvestable = isHarvestable;
        }

        private void OnMouseDown()
        {
            Debug.LogWarning($"OnPointerDown: {Data.PlantSprite.name}");
            if (!_isHarvestable)
            {
                return;
            }

            AudioController.Instance?.PlayHarvestSfx();
            Inventory.Instance.AddCollectedItem(Data.Name, Data.SellValue, Data.CollectibleSprite);
            Harvested?.Invoke(Position);
            Destroy(gameObject);
        }
    }
}