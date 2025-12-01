using Main.Plant;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

namespace Main.Soil
{
    [RequireComponent(typeof(Tilemap))]
    public class SoilTilemap : MonoBehaviour
    {
        public static SoilTilemap Instance { get; private set; }

        [SerializeField] private Camera mainCamera;
        [SerializeField] private Tilemap soilTilemap;
        [SerializeField] private PlantObject plantPrefab;

        [Header("Growth")]
        [SerializeField] private float growTime = 3f;

        public Tilemap Tilemap => soilTilemap;
        private Sprite _plantSprite;
        private Sprite _sproutSprite;
        private List<SoilCell> Cells { get; } = new();

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

            BuildCells();
        }

        private void BuildCells()
        {
            Debug.Log($"Cells Size: x={soilTilemap.size.x}, y={soilTilemap.size.y}");
            Cells.Clear();
            BoundsInt bounds = soilTilemap.cellBounds;
            foreach (var pos in bounds.allPositionsWithin)
            {
                TileBase tile = soilTilemap.GetTile(pos);
                if (tile != null)
                {
                    // For now: all painted tiles = plantable
                    SoilCell cell = new SoilCell(
                        pos.x,
                        pos.y,
                        CellState.Plantable
                    );

                    Cells.Add(cell);
                }
            }
        }

        public SoilCell GetCellAt(Vector3Int pos)
        {
            return Cells.Find(cell => cell.ComparePosition(pos));
        }

        public bool IsPlantable(Vector3Int pos)
        {
            SoilCell cell = GetCellAt(pos);
            return cell is { State: CellState.Plantable };
        }
        
        private void SetCellState(Vector3Int position, CellState state)
        {
            SoilCell cell = GetCellAt(position);
            cell?.SetState(state);
        }

        /// <summary>
        /// Try to plant at a world position. Returns true if planting succeeded.
        /// </summary>
        public bool TryPlantAt(Vector3 worldPos, Plant.Plant data)
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

        private void PlantAtCell(Vector3Int cell, Plant.Plant data)
        {
            Debug.LogError($"PlantAtCell: cell: x={cell.x}, y={cell.y}");
            if (!IsPlantable(cell))
            {
                Debug.LogError($"CANNOT PlantAtCell: cell: x={cell.x}, y={cell.y}");
                return;
            }

            Vector3 center = soilTilemap.GetCellCenterWorld(cell);
            var plantObject = Instantiate(plantPrefab, center, Quaternion.identity);
            plantObject.SetData(data, cell.y, cell, mainCamera);
            plantObject.Harvested += OnHarvested;
            SetCellState(cell, CellState.Unplantable);
        }

        private void OnHarvested(Vector3Int cellPosition)
        {
            SetCellState(cellPosition, CellState.Plantable);
        }
    }
}