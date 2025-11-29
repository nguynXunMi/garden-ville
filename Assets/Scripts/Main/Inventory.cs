using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private RectTransform itemParent;
        [SerializeField] private InventorySlot collectedImagePrefab;

        public static Inventory Instance;

        private Dictionary<string, int> CollectedDictionary { get; } = new();
        private List<InventorySlot> Slots { get; } = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        public void AddCollectedItem(Sprite sprite)
        {
            if (CollectedDictionary.ContainsKey(sprite.name))
            {
                CollectedDictionary[sprite.name] += 1;
                foreach (var slot in Slots)
                {
                    if (string.Equals(slot.SlotName, sprite.name))
                    {
                        slot.SetText($"{CollectedDictionary[sprite.name]}");
                    }
                }
            }
            else
            {
                var slot = Instantiate(collectedImagePrefab, itemParent);
                Slots.Add(slot);
                slot.SetImage(sprite);
                slot.SetText("1");
                CollectedDictionary[sprite.name] = 1;
            }
        }
    }
}