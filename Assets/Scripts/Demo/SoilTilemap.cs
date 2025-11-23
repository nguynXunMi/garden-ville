namespace Demo
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Tilemaps;

    public class SoilTilemap : MonoBehaviour
    {
        public static SoilTilemap Instance { get; private set; }

        [Header("Tilemap & sprites")] public Tilemap soilTilemap; // assign in inspector (Tilemap component)
        public Sprite sproutSprite; // world sprite for sprout
        public Sprite plantSprite; // world sprite for grown plant

        [Header("Growth")] public float growTime = 3f;

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
                soilTilemap = GetComponent<Tilemap>();

            if (soilTilemap == null)
                Debug.LogError("SoilTilemap: no Tilemap assigned or attached.");
        }

        /// <summary>
        /// Try to plant at a world position. Returns true if planting succeeded.
        /// </summary>
        public bool TryPlantAt(Vector3 worldPos)
        {
            if (soilTilemap == null) return false;

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

            PlantAtCell(cell);
            return true;
        }

        void PlantAtCell(Vector3Int cell)
        {
            Vector3 center = soilTilemap.GetCellCenterWorld(cell);
            GameObject go = new GameObject("Plant_" + cell.x + "_" + cell.y);
            go.transform.position = center;
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = sproutSprite;
            // optionally assign sorting layer/order
            StartCoroutine(GrowCoroutine(sr));
        }

        IEnumerator GrowCoroutine(SpriteRenderer sr)
        {
            yield return new WaitForSeconds(growTime);
            if (sr != null)
                sr.sprite = plantSprite;
        }
    }
}