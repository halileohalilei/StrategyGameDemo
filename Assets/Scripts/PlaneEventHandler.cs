using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class PlaneEventHandler : MonoBehaviour
    {
        private bool _isSelecting;

        private GameObject _oneByOneTile;
        private GameObject _twoByTwoTile;

        private Vector3 _lastMousePosition;
        private Vector3 _lastMouseOnWorldPosition;

        public GameObject OneByOneBuilding;

        private const float TOLERANCE = 0.001f;

        private void Awake()
        {
            _isSelecting = false;
            _oneByOneTile = GameObject.Find("OneByOneStructureTile");
            _twoByTwoTile = GameObject.Find("TwoByTwoStructureTile");
        }

        private void Update()
        {
            if (!_isSelecting) return;
            if (EventSystem.current.IsPointerOverGameObject())
            {
                OnMouseMove();
            }
        }

        public void OnMouseClick(BaseEventData data)
        {
            var pointerData = data as PointerEventData;
            if (pointerData != null && pointerData.pointerId == -1)
            {
                _isSelecting = !_isSelecting;
                _oneByOneTile.SetActive(_isSelecting);
            }
            else if (pointerData != null && pointerData.pointerId == -2)
            {
                if (!_isSelecting) return;
                var buildingPos = Util.WorldToGridPosition(_lastMouseOnWorldPosition);
                Instantiate(OneByOneBuilding, buildingPos, Quaternion.identity);
                Debug.Log("New 1x1 building at: " + Util.GridPositionToString(buildingPos));
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
                Vector3 tilePos = Util.WorldToGridPosition(cursor.worldPosition);
                _oneByOneTile.transform.position = tilePos;
                _lastMouseOnWorldPosition = cursor.worldPosition;
                Debug.Log("Selection tile on i: " + (tilePos.z - 0.5f) + " j: " + (tilePos.x - 0.5f));
            }
        }
    }
}