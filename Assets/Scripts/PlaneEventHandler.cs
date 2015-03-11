using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class PlaneEventHandler : MonoBehaviour {

    private bool isSelecting;

    private GameObject oneByOneTile;
    private GameObject twoByTwoTile;

    private Vector3 lastMousePosition;
    private Vector3 lastMouseOnWorldPosition;

    public GameObject OneByOneBuilding;

    void Awake()
    {
        isSelecting = false;
        oneByOneTile = transform.GetChild(0).gameObject;
        twoByTwoTile = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if (isSelecting)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                OnMouseMove();
            }
        }
    }

    public void OnMouseClick(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        if (pointerData.pointerId == -1)
        {
            isSelecting = !isSelecting;
            oneByOneTile.SetActive(isSelecting);
        }
        else if (pointerData.pointerId == -2)
        {
            if (isSelecting)
            {
                Vector3 buildingPos = worldToGridPosition(lastMouseOnWorldPosition);
                Instantiate(OneByOneBuilding, buildingPos, Quaternion.identity);
                Debug.Log("New 1x1 building at: " + printVector(buildingPos));
            }
        }
    }

    Vector3 worldToGridPosition(Vector3 worldPosition)
    {
        Vector3 gridPos = worldPosition;
        gridPos.y = 0.001f;
        int tileI = Mathf.FloorToInt(gridPos.z);
        int tileJ = Mathf.FloorToInt(gridPos.x);
        gridPos.z = tileI + 0.5f;
        gridPos.x = tileJ + 0.5f;

        return gridPos;
    }

    void OnMouseMove()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        if (lastMousePosition == null || Vector3.Distance(lastMousePosition, currentMousePosition) != 0f)
        {
            PointerEventData cursor = new PointerEventData(EventSystem.current);
            cursor.position = lastMousePosition = currentMousePosition;
            List<RaycastResult> objectsHit = new List<RaycastResult>();
            EventSystem.current.RaycastAll(cursor, objectsHit);
            Vector3 tilePos = worldToGridPosition(cursor.worldPosition);
            oneByOneTile.transform.position = tilePos;
            lastMouseOnWorldPosition = cursor.worldPosition;
            Debug.Log("Selection tile on i: " + (tilePos.z - 0.5f) + " j: " + (tilePos.x - 0.5f));
        }
    }

    string printVector(Vector3 v)
    {
        return "i: " + v.z + ", j: " + v.x;
    }
}
