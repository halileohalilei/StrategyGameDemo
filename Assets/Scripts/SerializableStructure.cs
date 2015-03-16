using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class SerializableStructure
    {
        [XmlAttribute("StartingZ")]
        public int StartingZ;
        [XmlAttribute("StartingX")]
        public int StartingX;
        [XmlAttribute("ZLength")]
        public int ZLength;
        [XmlAttribute("XLength")] 
        public int XLength;

        [XmlAttribute("StructureType")] 
        public int StructureType;

        private static GameObject dummy = new GameObject();

        public SerializableStructure(Structure s)
        {
            StartingZ = s.StartingZ;
            StartingX = s.StartingX;
            ZLength = s.ZLength;
            XLength = s.XLength;
            StructureType = s.GetStructureType();
        }

        public SerializableStructure()
        {
        }

        public Structure ToStructure()
        {
            Structure s = null;
            if (StructureType == (int) Structure.StructureType.Common)
            {
                s = dummy.AddComponent<CommonStructure>();
            }
            else if (StructureType == (int) Structure.StructureType.Unique)
            {
                s = dummy.AddComponent<UniqueStructure>();
            }
            s.StartingZ = StartingZ;
            s.StartingX = StartingX;
            s.ZLength = ZLength;
            s.XLength = XLength;
            return s;
        }
    }
}
