using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class PlaneEventHandler : MonoBehaviour
    {
        private GameObject _tileQuad;
//        private GameObject _twoByTwoTile;

        private Vector3 _lastMousePosition;
        private Vector3 _lastMouseOnWorldPosition;

//        public GameObject OneByOneBuilding;
        public GameObject Tile;

        private Grid grid;

        public GameObject[] allStructures;
        private int _selectedStructureIndex;

        private const float TOLERANCE = 0.001f;

        private void Awake()
        {
            _tileQuad = Instantiate(Tile, new Vector3(0, 0, 0), Quaternion.Euler(90f, 0f, 0f)) as GameObject;
            if (_tileQuad != null) _tileQuad.SetActive(false);

            grid = GetComponent<Grid>();
            _selectedStructureIndex = 0;
        }

        private void Update()
        {
            if (!GameData.IsPlacing()) return;
            if (EventSystem.current.IsPointerOverGameObject())
            {
                OnMouseMove();
            }
        }

        public void OnMouseClick(BaseEventData data)
        {
//            Debug.Log("click");
            PointerEventData pointerData = data as PointerEventData;
            if (pointerData != null && pointerData.pointerId == -1)
            {
                GameData.SwitchState();
                _tileQuad.SetActive(GameData.IsPlacing());
            }
            else if (pointerData != null && pointerData.pointerId == -2)
            {
                if (!GameData.IsPlacing()) return;
                Vector3 structurePos = Util.WorldToGridPosition(_lastMouseOnWorldPosition, grid);
                GameObject selectedStructure = allStructures[_selectedStructureIndex];
                Structure prefabStructure = selectedStructure.GetComponent<Structure>();
                if (grid.PositionOnGridAvailable(Convert.ToInt32(structurePos.z), 
                                                 Convert.ToInt32(structurePos.x), 
                                                 prefabStructure.ZLength, 
                                                 prefabStructure.XLength))
                {
                    GameObject newStructure = Instantiate(selectedStructure, structurePos, Quaternion.identity) as GameObject;
                    if (newStructure != null)
                    {
                        newStructure.transform.position = Util.DetermineStructurePosition(grid, structurePos,
                            prefabStructure);
                        Debug.Log("New 1x1 building at: " + Util.GridPositionToString(newStructure.transform.position));
                    }
//                    GameData.Instance.PutNewStructure(buildingPos, 1, 1, GameData.StructureType.Common);
                }
            }
        }

        private void OnMouseMove()
        {
            Vector3 currentMousePosition = Input.mousePosition;
            if (Math.Abs(Vector3.Distance(_lastMousePosition, currentMousePosition)) > TOLERANCE)
            {
                PointerEventData cursor = new PointerEventData(EventSystem.current)
                {
                    position = _lastMousePosition = currentMousePosition
                };
                List<RaycastResult> objectsHit = new List<RaycastResult>();
                EventSystem.current.RaycastAll(cursor, objectsHit);
                Vector3 tilePos = Util.WorldToGridPosition(cursor.worldPosition, grid);
                tilePos += new Vector3(0.5f * grid.TileXSize, 0, 0.5f * grid.TileZSize);
                _tileQuad.transform.position = tilePos;
                _lastMouseOnWorldPosition = cursor.worldPosition;
            }
        }
    }
}