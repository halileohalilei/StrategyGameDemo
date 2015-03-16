using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts
{
    [XmlRoot("GameData")]
    public class GameData
    {
        [XmlIgnore]
        public List<Structure> _allPlacedStructures = new List<Structure>();
        [XmlArray("Structures"), XmlArrayItem("Structure")]
        public List<SerializableStructure> sList = new List<SerializableStructure>();

        public static int NumberOfUniqueStructuresLeft = 15;
        public static int NumberOfCommonStructuresLeft = 5;

        private static GameData instance;

        static GameData()
        {
            instance = LoadPlacedStructures();
            instance._allPlacedStructures = new List<Structure>();
            List<Structure> tempStructureList = new List<Structure>();
            foreach (SerializableStructure ss in instance.sList)
            {
                Structure s = ss.ToStructure();
                tempStructureList.Add(s);
            }
            Grid grid = GameObject.Find("Plane").GetComponent<Grid>();
            grid.LoadStructuresFromLastSession(tempStructureList);
        }

        public static void Awake()
        {
            
        }

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

            instance._allPlacedStructures.Add(structure);
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

            instance._allPlacedStructures.Remove(structure);
            Object.Destroy(structure.gameObject);
            SavePlacedStructures();
        }

        private static void SavePlacedStructures()
        {
            instance.sList.Clear();
            foreach (Structure s in instance._allPlacedStructures)
            {
                SerializableStructure ss = new SerializableStructure(s);
                instance.sList.Add(ss);
            }

            var serializer = new XmlSerializer(typeof(GameData));
            var stream = new FileStream("placedStructures.xml", FileMode.Create);
            serializer.Serialize(stream, instance);
            stream.Close();
        }

        private static GameData LoadPlacedStructures()
        {
            Debug.Log("Loading started");
            var serializer = new XmlSerializer(typeof(GameData));
            FileStream stream;
            try
            {
                stream = new FileStream("placedStructures.xml", FileMode.Open);
                GameData data = serializer.Deserialize(stream) as GameData;
                stream.Close();

                Debug.Log("Loading done.");
                return data;
            }
            catch (FileNotFoundException)
            {
                return new GameData();
            }
            catch (XmlException)
            {
                return new GameData();
            }
        }
    }
}