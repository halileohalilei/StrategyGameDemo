using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class CommonStructure : Structure
    {
        public override int GetStructureType()
        {
            return (int) StructureType.Common;
        }

        public override void OnClick()
        {
            Debug.Log("This is a Common Structure.");
        }
    }
}
