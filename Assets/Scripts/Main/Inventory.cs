using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Main
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private RectTransform itemParent;
        [SerializeField] private InventorySlot collectedImagePrefab;
        [SerializeField] private TMP_Text goldText;

        public static Inventory Instance;

        [SerializeField] private Dictionary<string, int> CollectedDictionary = new();
        [SerializeField] private List<InventorySlot> Slots = new();

        public int PlayerGold { get; private set; }

        private void SetGold(int gold)
        {
            PlayerGold = gold;
            goldText.text = $"{gold}";
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            SetGold(0);
        }

        public void AddCollectedItem(string slotName, int sellValue, Sprite sprite)
        {
            if (CollectedDictionary.ContainsKey(slotName))
            {
                CollectedDictionary[slotName] += 1;
                foreach (var slot in Slots)
                {
                    if (string.Equals(slot.SlotName, slotName))
                    {
                        slot.SetText($"{CollectedDictionary[slotName]}");
                    }
                }
            }
            else
            {
                var slot = Instantiate(collectedImagePrefab, itemParent);
                Slots.Add(slot);
                slot.SetData(slotName, sellValue, sprite);
                slot.SetText("1");
                CollectedDictionary[slotName] = 1;
            }
        }

        public void SellItem(string slotName, int sellValue)
        {
            var quantity = CollectedDictionary[slotName];
            CollectedDictionary.Remove(slotName);
            var slot = Slots.Find(slot => string.Equals(slot.SlotName, slotName));
            if (slot != null)
            {
                Slots.Remove(slot);
            }

            SetGold(PlayerGold + sellValue * quantity);
        }

        public void OnPurchased(int price) => SetGold(PlayerGold - price);
    }
}