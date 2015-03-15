using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class CommonStructure : Structure
    {
        public override void Awake()
        {
            base.Awake();
            PopUpColor = new Color(0f, 0f, 0f, .5f);
        }
        public override int GetStructureType()
        {
            return (int) StructureType.Common;
        }

        public override void PrintStructureInfo()
        {
            string structureInfo = "This is a Common Structure. Location: (" + StartingZ +
                                   ", " + StartingX +
                                   ") Width: " + ZLength + " Height: " + XLength;
            Debug.Log(structureInfo);
        }
    }
}
