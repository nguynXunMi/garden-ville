using Main.Plant;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class Garden : MonoBehaviour
    {
        [SerializeField] private PlantData plantData;
        // [SerializeField] private SeedDraggable seedPrefab;
        [SerializeField] private SeedToTilemap seedPrefab;
        [SerializeField] private List<RectTransform> seedParents = new();

        private void Start()
        {
            LoadData();
        }

        private void LoadData()
        {
            if (plantData == null || plantData.Plants is not { Count: > 0 })
            {
                return;
            }

            for (var i = 0; i < seedParents.Count; ++i)
            {
                var seedParent = seedParents[i];
                var seed = Instantiate(seedPrefab, seedParent);
                seed.SetData(plantData.Plants[i]);
            }
        }
    }
}