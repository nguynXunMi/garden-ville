using System.Collections;
using System;
using Obsolete;
using UnityEngine.UI;
using UnityEngine;

namespace Main.Plant
{
    public class PlantObject : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Image progressBarImage;
        [SerializeField] private GameObject progressBar;

        private Plant Data { get; set; }
        private Vector3Int Position { get; set; }

        private bool _isGrowing = false;
        private bool _isHarvestable = false;
        private float growTime = 3f;
        private float _timerCount;

        public event Action<Vector3Int> Harvested;

        private void Update()
        {
            if (_isGrowing)
            {
                _timerCount += Time.deltaTime;
                progressBarImage.fillAmount = _timerCount / growTime;
            }
        }

        public void SetData(Plant data, Vector3Int position, Camera mainCamera)
        {
            canvas.worldCamera = mainCamera;
            Data = data;
            Position = position;
            spriteRenderer.sprite = data.SproutSprite;
            progressBar.gameObject.SetActive(true);
            _timerCount = 0f;
            _isGrowing = true;
            StartCoroutine(GrowCoroutine());
        }

        private IEnumerator GrowCoroutine()
        {
            yield return new WaitForSeconds(growTime);
            _isGrowing = false;
            progressBar.gameObject.SetActive(false);

            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = Data.PlantSprite;
                _isHarvestable = true;
            }
        }

        private void OnMouseDown()
        {
            Debug.LogWarning($"OnPointerDown: {Data.PlantSprite.name}");
            if (!_isHarvestable)
            {
                return;
            }

            Inventory.Instance.AddCollectedItem(Data.CollectibleSprite);
            Harvested?.Invoke(Position);
            Destroy(gameObject);
        }
    }
}