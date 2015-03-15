using UnityEngine;

namespace Assets.Scripts
{
    public class UIEventHandler : MonoBehaviour {

        public void RemoveStructure()
        {
            Structure.CurrentlySelectedStructure.GetComponent<Structure>().DeleteStructure();
        }

    }
}
