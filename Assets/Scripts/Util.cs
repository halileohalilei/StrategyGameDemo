using System;
using UnityEngine;

namespace Assets.Scripts
{
    class Util
    {
        public static Vector3 WorldToGridPosition(Vector3 worldPosition, Grid grid)
        {
            Vector3 gridPos = worldPosition;
            gridPos.y = 0.001f;
            gridPos.z = grid.StartingPoint.z + Mathf.Floor((worldPosition.z - grid.StartingPoint.z) / grid.TileZSize);
            gridPos.x = grid.StartingPoint.x + Mathf.Floor((worldPosition.x - grid.StartingPoint.x) / grid.TileXSize);
            return gridPos;
        }

        public static Vector3 DetermineStructurePosition(Grid grid, Vector3 startingIndices, Structure structure)
        {
            // z pozisyonu: (baslangic z'si) + (strcuture'in z'deki uzunlugu * tile genisligi)' in ortasi
            float z = grid.StartingPoint.z + startingIndices.z + (structure.ZLength*grid.TileZSize)/2;
            float x = grid.StartingPoint.x + startingIndices.x + (structure.XLength*grid.TileXSize)/2;

            return new Vector3(x, 0.001f, z);
        }
    }
}