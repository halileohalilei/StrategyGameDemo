using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class GameData
    {
        private List<GameObject> _allPlacedStructures = new List<GameObject>();
        private static GameState _state = GameState.GameStateIdle;

        private enum GameState
        {
            GameStateIdle,
            GameStatePlacingStructures
        }

        private static GameState State
        {
            set { _state = value; }
            get { return _state; }
        }

        public static void SwitchState()
        {
            State = IsPlacing() ? GameState.GameStateIdle : GameState.GameStatePlacingStructures;
        }
        public static bool IsPlacing()
        {
            return State == GameState.GameStatePlacingStructures;
        }

        public void AddStructure(GameObject structure)
        {
            _allPlacedStructures.Add(structure);
        }
    }
}