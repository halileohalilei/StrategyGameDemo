using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class PlaneEventHandler : MonoBehaviour
    {
        private GameObject _oneByOneTile;
//        private GameObject _twoByTwoTile;

        private Vector3 _lastMousePosition;
        private Vector3 _lastMouseOnWorldPosition;

        public GameObject OneByOneBuilding;
        public GameObject Tile;

        private const float TOLERANCE = 0.001f;

        private void Awake()
        {
//            _oneByOneTile = GameObject.Find("OneByOneStructureTile");
            _oneByOneTile = Instantiate(Tile, new Vector3(0, 0, 0), Quaternion.Euler(90f, 0f, 0f)) as GameObject;
            if (_oneByOneTile != null) _oneByOneTile.SetActive(false);
//            _twoByTwoTile = GameObject.Find("TwoByTwoStructureTile");
        }

        private void Update()
        {
            if (!GameData.Instance.IsPlacing()) return;
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
                GameData.Instance.SwitchState();
                _oneByOneTile.SetActive(GameData.Instance.IsPlacing());
            }
            else if (pointerData != null && pointerData.pointerId == -2)
            {
                if (!GameData.Instance.IsPlacing()) return;
                var buildingPos = Util.WorldToGridPosition(_lastMouseOnWorldPosition);
                if (GameData.Instance.PositionAvailable(buildingPos))
                {
                    Instantiate(OneByOneBuilding, buildingPos, Quaternion.identity);
                    Debug.Log("New 1x1 building at: " + Util.GridPositionToString(buildingPos));
                    GameData.Instance.PutNewStructure(buildingPos, 1, 1, GameData.StructureType.Common);
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
                Vector3 tilePos = Util.WorldToGridPosition(cursor.worldPosition);
                _oneByOneTile.transform.position = tilePos;
                _lastMouseOnWorldPosition = cursor.worldPosition;
//                Debug.Log("Selection tile on i: " + (tilePos.z - 0.5f) + " j: " + (tilePos.x - 0.5f));
            }
        }
    }
}