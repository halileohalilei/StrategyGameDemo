using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class PlaneEventHandler : MonoBehaviour {

    private bool isSelecting;

    private GameObject oneByOneTile;
    private GameObject twoByTwoTile;

    private Vector3 lastMousePosition;

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
                OnMouseMove();
        }
    }

    public void OnClick(BaseEventData data)
    {
        isSelecting = !isSelecting;
        oneByOneTile.SetActive(isSelecting);
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
            Vector3 tilePos = cursor.worldPosition;
            tilePos.y = 0.001f;
            int tileI = Mathf.FloorToInt(tilePos.z);
            int tileJ = Mathf.FloorToInt(tilePos.x);
            tilePos.z = tileI + 0.5f;
            tilePos.x = tileJ + 0.5f;
            oneByOneTile.transform.position = tilePos;
            Debug.Log("Selection tile on i: " + tileI + " j: " + tileJ);
        }
    }
}
