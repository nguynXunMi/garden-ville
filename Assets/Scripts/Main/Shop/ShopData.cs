using System.Collections.Generic;
using UnityEngine;

namespace Main.Shop
{
    [CreateAssetMenu(fileName = "Shop Data", menuName = "Shop/ShopData")]
    public class ShopData : ScriptableObject
    {
        [SerializeField] private List<ShopItemData> shopItemDataList = new();

        public List<ShopItemData> ShopItemDataList => shopItemDataList;
    }
}