using System;
using UnityEngine;

namespace Garden
{
    [Serializable]
    public class Plant
    {
        [SerializeField] private Sprite seedSprite;
        [SerializeField] private Sprite sproutSprite;
        [SerializeField] private Sprite plantSprite;
        [SerializeField] private Sprite collectibleSprite;

        public Sprite SeedSprite => seedSprite;
        public Sprite SproutSprite => sproutSprite;
        public Sprite PlantSprite => plantSprite;
        public Sprite CollectibleSprite => collectibleSprite;
    }
}