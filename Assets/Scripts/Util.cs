using System;
using UnityEngine;

namespace Assets.Scripts
{
    class Util
    {
        public static String GridPositionToString(Vector3 v)
        {
            return "i: " + v.z + ", j: " + v.x;
        }

        public static Vector3 WorldToGridPosition(Vector3 worldPosition)
        {
            Vector3 gridPos = worldPosition;
            gridPos.y = 0.001f;
            int tileI = Mathf.FloorToInt(gridPos.z);
            int tileJ = Mathf.FloorToInt(gridPos.x);
            gridPos.z = tileI + 0.5f;
            gridPos.x = tileJ + 0.5f;

            return gridPos;
        }
    }
}