using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts
{
    class GameData
    {
        private static List<GameObject> _allPlacedStructures = new List<GameObject>();

        public static void AddStructure(GameObject structure)
        {
            _allPlacedStructures.Add(structure);
            SavePlacedStructures();
        }

        public static void RemoveStructure(GameObject structure)
        {
            _allPlacedStructures.Remove(structure);
            Object.Destroy(structure);
            SavePlacedStructures();
        }

        private static void SavePlacedStructures()
        {
            
        }
    }
}