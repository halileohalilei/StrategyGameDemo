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

        private Vector3 _lastMousePosition;
        private Vector3 _lastMouseOnWorldPosition;

        public GameObject Tile;

        private Grid grid;

        public GameObject[] allStructures;
        private int _selectedStructureIndex;

        private const float TOLERANCE = 0.001f;

        void Awake()
        {
            _tileQuad = Instantiate(Tile, new Vector3(0, 0, 0), Quaternion.Euler(90f, 0f, 0f)) as GameObject;
            if (_tileQuad != null) _tileQuad.SetActive(false);

            grid = GetComponent<Grid>();
            _selectedStructureIndex = -1;
        }

        void Update()
        {
            if (!IsPlacingStructure()) return;
            if (EventSystem.current.IsPointerOverGameObject())
            {
                OnMouseMove();
            }
        }

        public void ChangeStructureType(int type)
        {
            if (_selectedStructureIndex == type)
            {
                _selectedStructureIndex = -1;
            }
            else
            {
                _selectedStructureIndex = type;
            }
            _tileQuad.SetActive(IsPlacingStructure());
        }

        public void OnMouseClick(BaseEventData data)
        {
            if (!IsPlacingStructure()) return;

            PointerEventData pointerData = data as PointerEventData;
            if (pointerData != null && pointerData.pointerId == -1)
            {
                if (!IsPlacingStructure()) return;
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
                        newStructure.transform.parent = transform;
                        Structure s = newStructure.GetComponent<Structure>();
                        s.StartingX = Convert.ToInt32(structurePos.x);
                        s.StartingZ = Convert.ToInt32(structurePos.z);
                        grid.AddStructureToGrid(s);

                    }
                }
            }
        }

        void OnMouseMove()
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

        private bool IsPlacingStructure()
        {
            return _selectedStructureIndex != -1;
        }

    }
}