using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class UniqueStructure : Structure
    {
        public override int GetStructureType()
        {
            return (int)StructureType.Unique;
        }
    }
}
