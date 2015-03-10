using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Tile : MonoBehaviour {

    public int i, j;

    public override string ToString()
    {
        return "Tile at position: " + i + ", " + j;
    }

    public void onClick(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;

        Debug.Log(pointerData.worldPosition);
    }
}
