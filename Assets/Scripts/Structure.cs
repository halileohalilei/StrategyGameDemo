using UnityEngine;

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

        public virtual int GetStructureType()
        {
            return 1;
        }

        public virtual void OnClick()
        {
            Debug.Log("This is a Structure.");
        }
    }
}
