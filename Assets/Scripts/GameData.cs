using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class GameData
    {
        private List<GameObject> _allPlacedStructures = new List<GameObject>();

        public void AddStructure(GameObject structure)
        {
            _allPlacedStructures.Add(structure);
        }
    }
}