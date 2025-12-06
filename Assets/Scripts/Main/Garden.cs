using System;
using Main.Plant;
using System.Collections.Generic;
using Main.Controllers;
using UnityEngine;

namespace Main
{
    public class Garden : MonoBehaviour
    {
        [SerializeField] private PlantData plantData;
        // [SerializeField] private SeedDraggable seedPrefab;
        [SerializeField] private SeedToTilemap seedPrefab;
        [SerializeField] private List<RectTransform> seedParents = new();

        // TODO: Move pause menu logic to somewhere else
        [SerializeField] private GameObject pauseMenu;

        // TODO: Move pause menu logic to somewhere else
        public void BackToMenuScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        
        private void Start()
        {
            AudioController.Instance?.PlayMainBGM();
            LoadData();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);
            }
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