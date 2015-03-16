using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    class UniqueStructure : Structure
    {
        public override void Awake()
        {
            base.Awake();
            PopUpColor = new Color(Random.value, Random.value, Random.value, .5f);
        }

        public override int GetStructureType()
        {
            return (int) StructureType.Unique;
        }

        public override void PrintStructureInfo()
        {
            string structureInfo = "This is a Unique Structure with color " + PopUpColor + ". Location: (" + StartingZ +
                                   ", " + StartingX +
                                   ") Width: " + ZLength + " Height: " + XLength;
            Debug.Log(structureInfo);
        }
    }
}