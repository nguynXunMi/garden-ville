using System;
using UnityEngine;

namespace Main.Plant
{
    [Serializable]
    public class Plant
    {
        [SerializeField] private string name;
        [SerializeField] private float sproutDuration;
        [SerializeField] private Sprite seedSprite;
        [SerializeField] private Sprite sproutSprite;
        [SerializeField] private Sprite plantSprite;
        [SerializeField] private Sprite collectibleSprite;

        public string Name => name;
        public float SproutDuration => sproutDuration;
        public Sprite SeedSprite => seedSprite;
        public Sprite SproutSprite => sproutSprite;
        public Sprite PlantSprite => plantSprite;
        public Sprite CollectibleSprite => collectibleSprite;
    }
}