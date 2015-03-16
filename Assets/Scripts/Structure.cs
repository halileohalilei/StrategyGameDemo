using System;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [Serializable]
    public class Structure : MonoBehaviour
    {
        public enum StructureType
        {
            Common = 1,
            Unique
        }
        [XmlAttribute("StartingZ")]
        public int StartingZ;
        [XmlAttribute("StartingX")]
        public int StartingX;
        [XmlAttribute("ZLength")]
        public int ZLength;
        [XmlAttribute("XLength")]
        public int XLength;

        [XmlIgnore]
        private PopUpContainer _popUpContainer;

        [XmlIgnore]
        public static GameObject CurrentlySelectedStructure;

        //        [XmlAttribute("PopUpColor")]
        [XmlIgnore]
        public Color PopUpColor;

        [XmlIgnore]
        public StructureType sType;

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
