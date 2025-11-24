using System.Collections;
using Garden;
using UnityEngine.Tilemaps;
using UnityEngine;

namespace Demo
{
    [RequireComponent(typeof(Tilemap))]
    public class SoilTilemap : MonoBehaviour
    {
        public static SoilTilemap Instance { get; private set; }

        [SerializeField] private Tilemap soilTilemap; // assign in inspector (Tilemap component)
        [SerializeField] private PlantObject plantPrefab;

        [Header("Growth")]
        [SerializeField] private float growTime = 3f;

        public Tilemap Tilemap => soilTilemap;
        private Sprite _plantSprite;
        private Sprite _sproutSprite;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning("Multiple SoilTilemap instances found. Using the first one.");
                enabled = false;
                return;
            }

            Instance = this;

            if (soilTilemap == null)
            {
                soilTilemap = GetComponent<Tilemap>();
            }

            if (soilTilemap == null)
            {
                Debug.LogError("SoilTilemap: no Tilemap assigned or attached.");
            }
        }

        /// <summary>
        /// Try to plant at a world position. Returns true if planting succeeded.
        /// </summary>
        public bool TryPlantAt(Vector3 worldPos, Plant data)
        {
            if (soilTilemap == null)
            {
                return false;
            }

            Vector3Int cell = soilTilemap.WorldToCell(worldPos);
            TileBase tile = soilTilemap.GetTile(cell);

            if (tile == null)
            {
                // no tile here
                return false;
            }

            // Optionally: check that the tile is a specific 'soil' tile type,
            // not any tile. That requires comparing with a Tile asset reference.
            // For now we assume non-null tile = plantable soil.

            PlantAtCell(cell, data);
            return true;
        }

        private void PlantAtCell(Vector3Int cell, Plant data)
        {
            Vector3 center = soilTilemap.GetCellCenterWorld(cell);
            var plantObject = Instantiate(plantPrefab, center, Quaternion.identity);
            plantObject.SetData(data);
        }
    }
}