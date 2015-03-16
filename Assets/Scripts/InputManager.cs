using UnityEngine;

namespace Assets.Scripts
{
    public class InputManager : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                
            }
        }

        public void OnScreenClick()
        {
            Event e = Event.current;
            if (e.isMouse)
            {
                Debug.Log(e);
            }
        }
    }
}