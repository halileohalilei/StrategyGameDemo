using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class UniqueStructure : Structure
    {
        public override int GetStructureType()
        {
            return (int) StructureType.Unique;
        }

        public override void OnClick()
        {
            RectTransform popUpPanel = GameObject.Find("PopUpPanel").GetComponent<RectTransform>();
            Transform popUpPosition = transform.GetChild(1);
            Vector3 popUpPositionOnViewport = Camera.main.WorldToViewportPoint(popUpPosition.position);
            popUpPanel.anchorMin = popUpPanel.anchorMax = popUpPositionOnViewport;
        }
    }
}