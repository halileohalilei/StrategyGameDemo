﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Grid : MonoBehaviour
    {
        public int XLength;
        public int ZLength;

        public Vector3 StartingPoint;

        private float _xSize;
        private float _zSize;

        private int[,] _tiles;

        public float TileXSize { get; set; }
        public float TileZSize { get; set; }

        private void Awake()
        {
            Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
            _xSize = mesh.bounds.size.x*transform.localScale.x;
            _zSize = mesh.bounds.size.z*transform.localScale.z;

            TileXSize = _xSize/XLength;
            TileZSize = _zSize/ZLength;

            _tiles = new int[ZLength, XLength];
            GameData.Awake();
        }

        public bool PositionOnGridAvailable(int z, int x, int zLength, int xLength)
        {
            bool available = true;

            for (int i = 0; i < zLength; i++)
            {
                if (z + i >= ZLength)
                {
                    available = false;
                    break;
                }

                for (int j = 0; j < xLength; j++)
                {
                    if (x + j >= XLength)
                    {
                        available = false;
                        break;
                    }

                    if (_tiles[i + z, j + x] != 0)
                    {
                        available = false;
                        break;
                    }
                }
            }

            if (!available) Debug.Log("Unavailable position!");
            return available;
        }

        public void AddStructureToGrid(Structure s)
        {
            for (int i = 0; i < s.ZLength; i++)
            {
                for (int j = 0; j < s.XLength; j++)
                {
                    _tiles[s.StartingZ + i, s.StartingX + j] = s.GetStructureType();
                }
            }
            GameData.AddStructure(s);
        }

        public void RemoveStructureFromGrid(Structure s)
        {
            for (int i = 0; i < s.ZLength; i++)
            {
                for (int j = 0; j < s.XLength; j++)
                {
                    _tiles[s.StartingZ + i, s.StartingX + j] = 0;
                }
            }
            GameData.RemoveStructure(s);
        }

        private void PrintGrid()
        {
            for (int i = 0; i < XLength; i++)
            {
                string s = "";
                for (int j = 0; j < ZLength; j++)
                {
                    s += _tiles[j, i] + ", ";
                }
                Debug.Log(s);
            }
        }

        public void LoadStructuresFromLastSession(List<Structure> structures)
        {
            GameObject[] prefabs = GameObject.Find("Canvas").GetComponent<UIEventHandler>().AllStructures;
            for (int i = 0; i < structures.Count; i++)
            {
                Structure s = structures[i];
                Vector3 structurePos = new Vector3(s.StartingX, 0.01f, s.StartingZ);
                GameObject selectedStructure = prefabs[s.GetStructureType() - 1];
                GameObject newStructure = Instantiate(selectedStructure, structurePos, Quaternion.identity) as GameObject;
                if (newStructure != null)
                {
                    newStructure.transform.position = Util.DetermineStructurePosition(this, structurePos, s);
                    s = newStructure.GetComponent<Structure>();
                    s.StartingX = Convert.ToInt32(structurePos.x);
                    s.StartingZ = Convert.ToInt32(structurePos.z);
                    AddStructureToGrid(s);
                    s.transform.parent = transform;
                }
            }
            Util.UpdateStructureButtonTexts(typeof(CommonStructure));
            Util.UpdateStructureButtonTexts(typeof(UniqueStructure));
        }
    }
}