using System;
using UnityEngine;

namespace Main.Plant
{
    public enum PlantType
    {
        Fruit,
        Vegetable,
    }
    
    [Serializable]
    public class Plant
    {
        [SerializeField] private string name;
        [SerializeField] private float sproutDuration;
        [SerializeField] private PlantType plantType;
        [SerializeField] private int sellValue;
        [SerializeField] private Sprite seedSprite;
        [SerializeField] private Sprite sproutSprite;
        [SerializeField] private Sprite plantSprite;
        [SerializeField] private Sprite collectibleSprite;

        public string Name => name;
        public float SproutDuration => sproutDuration;
        public PlantType Type => plantType;
        public int SellValue => sellValue;
        public Sprite SeedSprite => seedSprite;
        public Sprite SproutSprite => sproutSprite;
        public Sprite PlantSprite => plantSprite;
        public Sprite CollectibleSprite => collectibleSprite;
    }
}