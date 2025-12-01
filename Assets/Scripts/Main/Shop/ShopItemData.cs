using System;
using UnityEngine;
using PlantData = Main.Plant.Plant;

namespace Main.Shop
{
	[Serializable]
    public class ShopItemData
    {
        [SerializeField] private PlantData plant;
        [SerializeField] private Sprite sprite;
        [SerializeField] private int id;
        [SerializeField] private int price;

        public PlantData Plant => plant;
        public Sprite Sprite => sprite;
        public int Id => id;
        public int Price => price;
    }
}