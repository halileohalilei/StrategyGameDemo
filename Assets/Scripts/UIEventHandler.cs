using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class UIEventHandler : MonoBehaviour {

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
    }
}
