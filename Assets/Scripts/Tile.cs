using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class Tile : MonoBehaviour {

        public int I, J;

        public int Width, Height;

        public override string ToString()
        {
            return "Tile at position: " + I + ", " + J;
        }

        public void OnClick(BaseEventData data)
        {
            PointerEventData pointerData = data as PointerEventData;

            if (pointerData != null) Debug.Log(pointerData.worldPosition);
        }
    }
}
