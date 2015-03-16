using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class UIEventHandler : MonoBehaviour {

        private const float TOLERANCE = 0.001f;

        private Vector3 _lastMouseOnWorldPosition;
        private Vector3 _lastMousePosition;
        private Grid _grid;

        public GameObject[] AllStructures;
        public GameObject Plane;

        private GameObject _structureBeingDragged;

        void Awake()
        {
            _grid = Plane.GetComponent<Grid>();
        }

        public void RemoveStructure()
        {
            Structure s = Structure.CurrentlySelectedStructure.GetComponent<Structure>();
            Type type = s.GetType();
            s.DeleteStructure();
            Util.UpdateStructureButtonTexts(type);
        }

        public void PrintStructureInfo()
        {
            Structure.CurrentlySelectedStructure.GetComponent<Structure>().PrintStructureInfo();
        }

        public void DragStructure()
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
                if (objectsHit.Count > 0 
                    && objectsHit[objectsHit.Count - 1].gameObject == Plane
                   )
                {
                    Structure prefabStructure = _structureBeingDragged.GetComponent<Structure>();
                    Vector3 structPos = Util.WorldToGridPosition(cursor.worldPosition, _grid);

                    if (_grid.PositionOnGridAvailable(Convert.ToInt32(structPos.z),
                                                      Convert.ToInt32(structPos.x),
                                                      prefabStructure.ZLength,
                                                      prefabStructure.XLength))
                    {
                        if (!_structureBeingDragged.activeSelf)
                        {
                            _structureBeingDragged.SetActive(true);
                        }
                        _lastMouseOnWorldPosition = cursor.worldPosition;
                        _structureBeingDragged.transform.position = Util.DetermineStructurePosition(_grid, structPos,
                            prefabStructure);
                    }
                }
                else if (_structureBeingDragged.activeSelf)
                {
                    _structureBeingDragged.SetActive(false);
                }
            }
        }

        public void EndDragStructure()
        {
            if (!_structureBeingDragged.activeSelf)
            {
                Destroy(_structureBeingDragged);
            }
            else
            {
                Vector3 structPos = Util.WorldToGridPosition(_lastMouseOnWorldPosition, _grid);
                _structureBeingDragged.transform.parent = Plane.transform;
                Structure s = _structureBeingDragged.GetComponent<Structure>();
                s.StartingX = Convert.ToInt32(structPos.x);
                s.StartingZ = Convert.ToInt32(structPos.z);
                _grid.AddStructureToGrid(s);
                Util.UpdateStructureButtonTexts(s.GetType());
            }
        }
        public void BeginDragStructure(GameObject structurePrefab)
        {
            if (structurePrefab.GetType() == typeof(UniqueStructure)
                    && GameData.NumberOfUniqueStructuresLeft == 0)
                return;
            if (structurePrefab.GetType() == typeof(CommonStructure)
                && GameData.NumberOfCommonStructuresLeft == 0)
                return;

            _structureBeingDragged = Instantiate(structurePrefab, structurePrefab.transform.position, Quaternion.identity) as GameObject;
            if (_structureBeingDragged != null)
            {
                _structureBeingDragged.SetActive(false);
            }
            Debug.Log("Began Drag");
        }
    }
}
