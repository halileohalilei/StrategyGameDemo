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
        public override int GetStructureType()
        {
            return (int) StructureType.Common;
        }
    }
}
