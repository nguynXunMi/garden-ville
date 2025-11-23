using System;
using UnityEngine;

namespace Garden
{
    [Serializable]
    public class Plant
    {
        [SerializeField] private Sprite plantSprite;
        [SerializeField] private Sprite seedSprite;
        [SerializeField] private string plantName;

        public Sprite PlantSprite => plantSprite;
        public Sprite SeedSprite => seedSprite;
        public string PlantName => plantName;
    }
}