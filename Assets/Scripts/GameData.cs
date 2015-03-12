using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    internal class GameData
    {
        private static GameData _instance = new GameData();

        public int[,] Grid;
        private int _gridWidth, _gridHeight;

        private enum GameState
        {
            GameStateIdle,
            GameStatePlacingStructures
        }

        public enum StructureType
        {
            Common = 1,
            Unique
        }

        private GameState State { set; get; }

        private GameData()
        {
            State = GameState.GameStateIdle;
            _gridWidth = 20;
            _gridHeight = 20;
            Grid = new int[_gridHeight, _gridWidth];
        }
        public static GameData Instance
        {
            get { return _instance; }
        }

        public void SwitchState()
        {
            State = IsPlacing() ? GameState.GameStateIdle : GameState.GameStatePlacingStructures;
        }

        public bool IsPlacing()
        {
            return State == GameState.GameStatePlacingStructures;
        }

        public bool PositionAvailable(Vector3 pos)
        {
            int i = Convert.ToInt32(pos.x) + _gridHeight/2 + 1;
            int j = Convert.ToInt32(pos.z) + _gridWidth/2 + 1;
            return Grid[i, j] == 0;
        }

        public void PutNewStructure(Vector3 pos, int structWidth, int structHeight, StructureType type)
        {
            for (int i = 0; i < structHeight; i++)
            {
                for (int j = 0; j < structWidth; j++)
                {
                    int ii = Convert.ToInt32(pos.x) + i + _gridHeight/2;
                    int jj = Convert.ToInt32(pos.z) + j + _gridWidth/2;
                    Grid[ii, jj] = (int) type;
                }
            }
        }
    }
}