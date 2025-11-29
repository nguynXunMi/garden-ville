using System.Collections.Generic;
using UnityEngine;

namespace Main.Plant
{
    [CreateAssetMenu(menuName = "PlantData")]
    public class PlantData : ScriptableObject
    {
        [SerializeField] private List<Plant> plants = new();

        public List<Plant> Plants => plants;
    }
}