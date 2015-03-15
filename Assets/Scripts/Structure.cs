using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Structure : MonoBehaviour
    {
        protected enum StructureType
        {
            Common = 1,
            Unique
        }

        public int StartingZ;
        public int StartingX;
        public int ZLength;
        public int XLength;

        protected PopUpContainer _popUpContainer;

        public static GameObject CurrentlySelectedStructure;

        public Color PopUpColor;

        public virtual void Awake()
        {
            _popUpContainer = GameObject.Find("PopUpContainer").GetComponent<PopUpContainer>();
        }

        public virtual int GetStructureType()
        {
            return 1;
        }

        public virtual void OpenPopUp()
        {
            RectTransform popUpPanel = _popUpContainer.PopUpPanel.GetComponent<RectTransform>();
            if (!popUpPanel.gameObject.activeSelf)
            {
                popUpPanel.gameObject.SetActive(true);
            }
            Transform popUpPosition = transform.GetChild(1);
            Vector3 popUpPositionOnViewport = Camera.main.WorldToViewportPoint(popUpPosition.position);
            popUpPanel.anchorMin = popUpPanel.anchorMax = popUpPositionOnViewport;
            popUpPanel.GetComponent<Image>().color = PopUpColor;

            CurrentlySelectedStructure = gameObject;
        }

        public void DeleteStructure()
        {
            Grid grid = transform.parent.GetComponent<Grid>();
            grid.RemoveStructureFromGrid(this);
            RectTransform popUpPanel = _popUpContainer.PopUpPanel.GetComponent<RectTransform>();
            popUpPanel.gameObject.SetActive(false);
            CurrentlySelectedStructure = null;
        }

        public virtual void PrintStructureInfo()
        {
            Debug.Log("This is a structure.");
        }
    }
}
