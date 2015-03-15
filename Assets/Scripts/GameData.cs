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
        private static List<Structure> _allPlacedStructures = new List<Structure>();

        public static int NumberOfUniqueStructuresLeft = 15;
        public static int NumberOfCommonStructuresLeft = 5;

        

        public static void AddStructure(Structure structure)
        {
            if (structure.GetType() == typeof(UniqueStructure))
            {
                NumberOfUniqueStructuresLeft--;
            }
            else if (structure.GetType() == typeof(CommonStructure))
            {
                NumberOfCommonStructuresLeft--;
            }

            _allPlacedStructures.Add(structure);
            SavePlacedStructures();
        }

        public static void RemoveStructure(Structure structure)
        {
            if (structure.GetType() == typeof(UniqueStructure))
            {
                NumberOfUniqueStructuresLeft++;
            }
            else if (structure.GetType() == typeof(CommonStructure))
            {
                NumberOfCommonStructuresLeft++;
            }

            _allPlacedStructures.Remove(structure);
            Object.Destroy(structure.gameObject);
            SavePlacedStructures();
        }

        private static void SavePlacedStructures()
        {
            
        }
    }
}