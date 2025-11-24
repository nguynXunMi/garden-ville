using System.Collections;
using Garden;
using UnityEngine;
using UnityEngine.UI;

namespace Demo
{
    public class PlantObject : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private float growTime = 3f;
        private Plant Data { get; set; }

        public void SetData(Plant data)
        {
            Data = data;
            spriteRenderer.sprite = data.SproutSprite;
            StartCoroutine(GrowCoroutine());
        }

        private IEnumerator GrowCoroutine()
        {
            yield return new WaitForSeconds(growTime);
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = Data.PlantSprite;
            }
        }
    }
}